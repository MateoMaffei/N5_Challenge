namespace N5_Challenge.Models.Request
{
    public class ModificarPermisoRequest
    {
        public Guid IdPermiso { get; set; }
        public Guid IdEmpleado { get; set; }
        public Guid IdNuevoTipoPermiso { get; set; }
    }
}
