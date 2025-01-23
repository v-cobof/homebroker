using Homebroker.Domain;
using Homebroker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace Homebroker.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : Entity, new()
    {
        protected readonly HomebrokerDbContext Db;
        protected readonly DbSet<T> DbSet;

        public IUnitOfWork UnitOfWork { get => Db; }

        public Repository(HomebrokerDbContext db)
        {
            Db = db;
            DbSet = db.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Create(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task Update(T entity)
        {
            DbSet.Update(entity);
        }

        public async Task<List<T>> GetByFilterAsync(Expression<Func<T, bool>> filter)
        {
            return await DbSet.AsNoTracking().Where(filter).ToListAsync();
        }
    }
}
