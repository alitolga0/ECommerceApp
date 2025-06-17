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
        [Authorize(Roles = "Admin")]
        public IDataResult<List<Order>> GetAll()
        {
            return _orderService.GetAll();
        }

        // Kullanıcının kendi siparişlerini listeler
        [HttpGet("GetAllByUser")]
        [Authorize(Roles = "User")]
        public IDataResult<List<Order>> GetAllByUser()
        {
            var userId = GetUserIdFromClaims();
            return _orderService.GetAll(o => o.UserId == userId);
        }

        // Kullanıcı kendi siparişini görür, admin tüm siparişleri görebilir ama bu endpoint sadece user için
        [HttpGet("GetById")]
        [Authorize(Roles = "User")]
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

        [HttpPost("Add")]
        [Authorize(Roles = "User")]
        public async Task<IResult> Add(Order entity)
        {
            var userId = GetUserIdFromClaims();
            entity.UserId = userId;
            return await _orderService.Add(entity);
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IResult> Update(Order entity)
        {
            return await _orderService.Update(entity);
        }

        [HttpPost("Delete")]
        [Authorize(Roles = "User")]
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
