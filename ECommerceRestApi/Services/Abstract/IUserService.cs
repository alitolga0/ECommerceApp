using ECommerceRestApi.Core.Service;
using ECommerceRestApi.Core.Utilities.Result;
using ECommerceRestApi.Models;
using System.Linq.Expressions;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
namespace ECommerceRestApi.Services.Abstract
{
    public interface IUserService 
    {
       
        IDataResult<User> GetById(Guid id);
        IDataResult<List<User>> GetAll(Expression<Func<User, bool>>? filter = null);
       
    }
}
