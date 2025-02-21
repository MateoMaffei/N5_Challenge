using N5_Challenge.Models.Entities;
using N5_Challenge.Models.Response;
using System.Linq.Expressions;

namespace N5_Challenge.Repositories.Interfaces
{
    public interface IPermisoRepository : IRepository<Permiso>
    {
        //Task SaveChangeAsync();
        //Task<Permiso?> GetByIdGuid(Guid id);
        //Task<IEnumerable<ObtenerPermisosResponse>> GetAsync();
        Task<Permiso?> GetPermisoByFKeys(int idEmpleado, int idTipoPermiso);
        //Task CreateAsync(int idEmpleado, int idTipoPermiso);

    }
}
