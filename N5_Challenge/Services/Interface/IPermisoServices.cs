using N5_Challenge.Models.Request;
using N5_Challenge.Models.Response;

namespace N5_Challenge.Services.Interface
{
    public interface IPermisoServices
    {
        Task<IEnumerable<ObtenerPermisosResponse>> GetPermisosAsync();
        Task CrearNuevoPermiso(SolicitarPermisosRequest solicitarPermiso);
        Task ModificarPermiso(ModificarPermisoRequest modificarPermiso);
        Task AprobarPermisosAsync();
    }
}
