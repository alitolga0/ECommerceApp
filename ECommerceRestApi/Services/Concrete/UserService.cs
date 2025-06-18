using Microsoft.AspNetCore.Identity;
using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using ECommerceRestApi.Core.Utilities.Result;
using System.Linq.Expressions;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
using ECommerceRestApi.Core.Repository;

namespace ECommerceRestApi.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

     
        public IDataResult<User> GetById(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == id);
            return new SuccessDataResult<User>(user,"başarıyla listelendi");
        }

        public IDataResult<List<User>> GetAll(Expression<Func<User, bool>>? filter = null)
        {
            var query = _userManager.Users;
            var users = filter != null ? query.Where(filter).ToList() : query.ToList();
            return new SuccessDataResult<List<User>>(users ,"başarıyla listelendi");
        }

       
    }
}
