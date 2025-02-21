using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Models.Entities;
using N5_Challenge.Repositories.Interfaces;
using System.Linq.Expressions;

namespace N5_Challenge.Repositories
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        private readonly ApplicationDbContext _context;

        public EmpleadoRepository(ApplicationDbContext context) : base(context)
        {
            {
                _context = context;
            }
        }
    }
}
