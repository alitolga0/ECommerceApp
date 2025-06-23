using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceRestApi.Core.Utilities.Result;
using System.Security.Claims;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;

namespace ECommerceRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAll")]
        
        public IDataResult<List<Order>> GetAll()
        {
            return _orderService.GetAll();
        }

      
        [HttpGet("GetAllByUser")]

        public IDataResult<List<Order>> GetAllByUser()
        {
            var userId = GetUserIdFromClaims();
            return _orderService.GetAll(o => o.UserId == userId);
        }

        [HttpGet("GetById")]

        public IDataResult<Order> GetById(Guid id)
        {
            var userId = GetUserIdFromClaims();

            var orderResult = _orderService.GetById(id);
            var order = orderResult.Data;

            if (order == null)
                return new ErrorDataResult<Order>("Sipariş bulunamadı");

            if (order.UserId != userId)
                return new ErrorDataResult<Order>("Yetkiniz olmayan sipariş");

            return orderResult;
        }
        [HttpGet("GetAllByUserWithDetails")]
        public IDataResult<List<Order>> GetAllByUserWithDetails()
        {
            var userId = GetUserIdFromClaims();
            var orders = _orderService.GetAllByUserWithDetails(userId);
            return new SuccessDataResult<List<Order>>(orders, "Siparişler başarıyla getirildi.");
        }

        [HttpPost("Add")]
        public async Task<IResult> Add(Order entity)
        {
            var userId = GetUserIdFromClaims();
            entity.UserId = userId;
            return await _orderService.Add(entity);
        }

        [HttpPost("Update")]

        public async Task<IResult> Update(Order entity)
        {
            return await _orderService.Update(entity);
        }

        [HttpPost("Delete")]

        public async Task<IResult> Delete(Guid id)
        {
            var userId = GetUserIdFromClaims();

            var orderResult = _orderService.GetById(id);
            var order = orderResult.Data;

            if (order == null)
                return new ErrorResult("Sipariş bulunamadı");

            if (order.UserId != userId)
                return new ErrorResult("Yetkiniz olmayan siparişi silemezsiniz");

            return await _orderService.Delete(id);
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
            {
                throw new UnauthorizedAccessException("Kullanıcı ID alınamadı.");
            }
            return userId;
        }
    }
}
