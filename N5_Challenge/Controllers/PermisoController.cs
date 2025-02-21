using Microsoft.AspNetCore.Mvc;
using N5_Challenge.Models.Request;
using N5_Challenge.Repositories.Interfaces;
using N5_Challenge.Services.Interface;

namespace N5_Challenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoServices _permisoServices;
        public PermisoController(IPermisoServices permisoServices)
        {
            _permisoServices = permisoServices;
        }

        [HttpGet]
        [Route("Obtener")]
        public async Task<IActionResult> ObtenerPermisoAsync()
        {
            var response = await _permisoServices.GetPermisosAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("Solicitar")]
        public async Task<IActionResult> SolicitarPermisoAsync(SolicitarPermisosRequest request)
        {
            await _permisoServices.CrearNuevoPermiso(request);

            return Ok("Permiso solicitado correctamente, aguarde a que sea aprobado.");
        }

        [HttpPut]
        [Route("Modificar")]
        public async Task<IActionResult> ModificarPermisoAsync(ModificarPermisoRequest request)
        {
            await _permisoServices.ModificarPermiso(request);
            return Ok();
        }


    }
}
