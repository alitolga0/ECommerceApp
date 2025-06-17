using ECommerceRestApi.Core.Utilities.Result;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
using System.Linq.Expressions;

namespace ECommerceRestApi.Core.Service
{
    public interface IBaseService<TEntity, TKey> where TEntity : class
    {
        Task<IResult> Add(TEntity entity);
        Task<IResult> Update(TEntity entity);
        Task<IResult> Delete(TKey id);
        IDataResult<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null);
        IDataResult<TEntity> GetById(TKey id);
    }

}
