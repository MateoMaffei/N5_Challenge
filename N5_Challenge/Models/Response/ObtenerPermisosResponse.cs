using N5_Challenge.Models.Entities;

namespace N5_Challenge.Models.Response
{
    public class ObtenerPermisosResponse
    {
        public Guid IdGuidEmpleado { get; set; }
        public string NombreApellido { get; set; }
        public string DescripcionTipoPermiso { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public bool Estado { get; set; }

        public static explicit operator ObtenerPermisosResponse(Permiso p)
        {
            return new ObtenerPermisosResponse
            {
                DescripcionTipoPermiso = p.TipoPermiso.Descripcion,
                Estado = p.Estado,
                FechaAprobacion = p.FechaAprobacion,
                FechaSolicitud = p.FechaSolicitud,
                IdGuidEmpleado = p.Empleado.IdGuid,
                NombreApellido = $"{p.Empleado.Apellido}, {p.Empleado.Nombre}"
            };
        }

    }
}
