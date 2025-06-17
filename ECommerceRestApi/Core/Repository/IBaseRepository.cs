using ECommerceRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ECommerceRestApi.Core.Repository
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        List<T> GetAllWithNavigation(Expression<Func<T, bool>>? filter = null, params string[] navigations);
        T? Get(Expression<Func<T, bool>> filter);
        T? GetWithNavigation(Expression<Func<T, bool>>? filter = null, params string[] navigations);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
    }
}
