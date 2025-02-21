using N5_Challenge.Exeptions;
using N5_Challenge.Models.Entities;
using N5_Challenge.Models.Request;
using N5_Challenge.Models.Response;
using N5_Challenge.Repositories.Interfaces;
using N5_Challenge.Services.Interface;
using static N5_Challenge.Helper.Enums;

namespace N5_Challenge.Services
{
    public class PermisoServices : IPermisoServices
    {
        private readonly IPermisoRepository _permisoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly ITipoPermisoRepository _tipoPermisoRepository;
        private readonly IKafkaProducerServices _kafkaServices;
        private readonly IElasticService _elasticService;
        private readonly ILogger<IPermisoServices> _logger;

        public PermisoServices(IPermisoRepository permisoRepository, 
            IEmpleadoRepository empleadoRepository,
            ITipoPermisoRepository tipoPermisoRepository,
            IKafkaProducerServices kafkaServices,
            ILogger<IPermisoServices> logger,
            IElasticService elasticService)
        {
            _permisoRepository = permisoRepository;
            _empleadoRepository = empleadoRepository;
            _tipoPermisoRepository = tipoPermisoRepository;
            _kafkaServices = kafkaServices;
            _elasticService = elasticService;
            _logger = logger;
        }

        public async Task<IEnumerable<ObtenerPermisosResponse>> GetPermisosAsync()
        {
            await _kafkaServices.EnviarEventoAsync(TipoOperacionesKafka.get);
            var respuesta = await _permisoRepository.GetAllAsync();

            return respuesta.Select(opr => (ObtenerPermisosResponse)opr);
        }

        public async Task CrearNuevoPermiso(SolicitarPermisosRequest solicitarPermiso)
        {
            try
            {
                var empleado = await _empleadoRepository.GetByIdGuidAsync(solicitarPermiso.IdEmpleado);
                var tipoPermiso = await _tipoPermisoRepository.GetByIdGuidAsync(solicitarPermiso.IdTipoPermiso);

                if (empleado == null || tipoPermiso == null)
                    throw new ForbidenException("Se produjo un error al crear la solicitud.");

                if (await ValidarPermisoSolicitado(empleado.Id, tipoPermiso.Id))
                    throw new ForbidenException("Ya se encuentra una solicitud para el usuario y el tipo de permiso solicitado, aguarde a que sea aprobado.");

                var permiso = new Permiso
                {
                    IdGuid = Guid.NewGuid(),
                    IdEmpleado = empleado.Id,
                    IdTipoPermiso = tipoPermiso.Id,
                    FechaSolicitud = DateTime.Now,
                    Estado = false,
                    FechaAprobacion = null,
                    FechaVencimiento = null
                };

                await _permisoRepository.AddAsync(permiso);
                await _permisoRepository.SaveChangesAsync();
                await RegistrarEventoYCrearIndice(permiso, TipoOperacionesKafka.request);
            }
            catch (UncontroledException) { throw; }
        }
        public async Task ModificarPermiso(ModificarPermisoRequest modificarPermiso)
        {
            try
            {
                var permiso = await _permisoRepository.GetByIdGuidAsync(modificarPermiso.IdPermiso);

                if (permiso == null)
                    throw new ForbidenException("No se encontro el permiso a actualizar.");

                if (permiso.Empleado.IdGuid != modificarPermiso.IdEmpleado)
                    throw new ForbidenException("El permiso no corresponde al empleado.");

                var nuevoTipo = await _tipoPermisoRepository.GetByIdGuidAsync(modificarPermiso.IdNuevoTipoPermiso);

                if (nuevoTipo == null)
                    throw new ForbidenException("No se encuentra el nuevo tipo de permiso solicitado");

                if (await ValidarPermisoSolicitado(permiso.Empleado.Id, nuevoTipo.Id))
                    throw new ForbidenException("Ya se solicito un permiso de este tipo para el empleado seleccionado.");

                permiso.IdTipoPermiso = nuevoTipo.Id;

                await _permisoRepository.SaveChangesAsync();
                await _kafkaServices.EnviarEventoAsync(TipoOperacionesKafka.modify);
            }
            catch (UncontroledException) { throw; }
        }

        private async Task<bool> ValidarPermisoSolicitado(int idEmpleado, int idTipoPermiso)
        {
            return await _permisoRepository.GetPermisoByFKeys(idEmpleado, idTipoPermiso) == null ? false : true;
        }

        private async Task RegistrarEventoYCrearIndice(Permiso p, TipoOperacionesKafka operacion)
        {
            await _kafkaServices.EnviarEventoAsync(operacion);
            await _elasticService.IndexDocumentAsync(p);
        }

        public async Task AprobarPermisosAsync()
        {
            _logger.LogInformation("Antes de buscar los permisos sin aprobar.");
            var permisosSinAprobar = await _permisoRepository.FindAsync(x => x.Estado == false);

            foreach (var permiso in permisosSinAprobar)
            {
                _logger.LogInformation($"Actualizando permiso con Id: {permiso.IdGuid}");
                permiso.FechaAprobacion = DateTime.Now;
                permiso.Estado = true;
                permiso.FechaVencimiento = DateTime.Now.AddDays(30);
                _permisoRepository.Update(permiso);
            }

            await _permisoRepository.SaveChangesAsync();
        }
    }
}
