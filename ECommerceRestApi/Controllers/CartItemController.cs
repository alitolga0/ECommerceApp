using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceRestApi.Core.Utilities.Result;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;

namespace ECommerceRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize] 
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpGet("GetAll")]
        public IDataResult<List<CartItem>> GetAll()
        {
            return _cartItemService.GetAll();
        }

        [HttpGet("GetById")]
        public IDataResult<CartItem> GetById(Guid id)
        {
            return _cartItemService.GetById(id);
        }

        [HttpPost("Add")]
        public async Task<IResult> Add(CartItem entity)
        {
            return await _cartItemService.Add(entity);
        }

        [HttpPost("Update")]
        public async Task<IResult> Update(CartItem entity)
        {
            return await _cartItemService.Update(entity);
        }

        [HttpPost("Delete")]
        public async Task<IResult> Delete(Guid id)
        {
            return await _cartItemService.Delete(id);
        }
    }
}
