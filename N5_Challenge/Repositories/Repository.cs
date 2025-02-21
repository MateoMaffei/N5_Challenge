using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using N5_Challenge.Repositories.Interfaces;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace N5_Challenge.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task<T?> GetByIdGuidAsync(Guid idGuid)
        {
            var property = typeof(T).GetProperty("IdGuid", BindingFlags.Public | BindingFlags.Instance);
            if (property == null)
                throw new InvalidOperationException($"La entidad {typeof(T).Name} no tiene una propiedad 'IdGuid'");

            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "IdGuid") == idGuid);
        }

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}