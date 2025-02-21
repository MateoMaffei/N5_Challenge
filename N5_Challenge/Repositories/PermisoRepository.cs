using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Models.Entities;
using N5_Challenge.Models.Response;
using N5_Challenge.Repositories.Interfaces;
using N5_Challenge.Services.Interface;

namespace N5_Challenge.Repositories
{
    public class PermisoRepository : Repository<Permiso>, IPermisoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IElasticService _elasticService;
        public PermisoRepository(ApplicationDbContext context, IElasticService elasticService) : base(context)
        {
            _context = context;
            _elasticService = elasticService;
        }

        public async Task<Permiso?> GetPermisoByFKeys(int idEmpleado, int idTipoPermiso)
        {
            return await _context.Permiso
                .Where(x => x.IdTipoPermiso == idTipoPermiso && x.IdEmpleado == idEmpleado)
                .FirstOrDefaultAsync();
        }
    }
}
