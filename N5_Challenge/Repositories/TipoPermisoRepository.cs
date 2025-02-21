using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Models.Entities;
using N5_Challenge.Repositories.Interfaces;

namespace N5_Challenge.Repositories
{
    public class TipoPermisoRepository : Repository<TipoPermiso>, ITipoPermisoRepository
    {
        private readonly ApplicationDbContext _context;

        public TipoPermisoRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
