using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using ECommerceRestApi.Core.Utilities.Result;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;

namespace ECommerceRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAll")]
        public IDataResult<List<User>> GetAll()
        {
            return _userService.GetAll();
        }

        [HttpGet("GetById")]
        public IDataResult<User> GetById(Guid id)
        {
            return _userService.GetById(id);
        }

       
    }
}
