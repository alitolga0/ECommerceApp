namespace ECommerceRestApi.Repository
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using global::ECommerceRestApi.Core.Repository;
    using global::ECommerceRestApi.Models;

    namespace ECommerceRestApi.Repository
    {
        public class BaseRepository<TEntity> : IBaseRepository<TEntity>
            where TEntity : BaseEntity, new()
        {
            

            private readonly MainDbContext _context;

            public BaseRepository(MainDbContext context)
            {
                _context = context;
            }


            public async Task Add(TEntity entity)
            {
                entity.Id = Guid.NewGuid();
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;

                await _context.Set<TEntity>().AddAsync(entity);
            }

            public async Task Delete(Guid id)
            {
                var entity = Get(x => x.Id == id);
                if (entity == null)
                    throw new Exception($"Entity with id '{id}' not found.");

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;

                _context.Set<TEntity>().Update(entity);
                await Task.CompletedTask;
            }

            public TEntity? Get(Expression<Func<TEntity, bool>> filter)
            {
                return _context.Set<TEntity>().FirstOrDefault(filter);
            }

            public TEntity? GetWithNavigation(Expression<Func<TEntity, bool>>? filter = null, params string[] navigations)
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                foreach (var navigation in navigations)
                {
                    query = query.Include(navigation);
                }

                return filter == null ? query.FirstOrDefault() : query.FirstOrDefault(filter);
            }

            public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
            {
                return filter == null
                    ? _context.Set<TEntity>().ToList()
                    : _context.Set<TEntity>().Where(filter).ToList();
            }

            public List<TEntity> GetAllWithNavigation(Expression<Func<TEntity, bool>>? filter = null, params string[] navigations)
            {
                IQueryable<TEntity> query = _context.Set<TEntity>();

                foreach (var navigation in navigations)
                {
                    query = query.Include(navigation);
                }

                return filter == null ? query.ToList() : query.Where(filter).ToList();
            }

            public async Task Update(TEntity entity)
            {
                var existingEntity = _context.Set<TEntity>().Find(entity.Id);

                if (existingEntity == null)
                    throw new Exception($"Entity with id '{entity.Id}' not found.");

                entity.CreatedAt = existingEntity.CreatedAt;  
                entity.IsDeleted = existingEntity.IsDeleted;
                entity.DeletedAt = existingEntity.DeletedAt;
                entity.UpdatedAt = DateTime.UtcNow;

                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                await Task.CompletedTask;
            }
        }
    }

}
