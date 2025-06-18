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
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;  

        public OrderItemController(IOrderItemService orderItemService, IOrderService orderService)
        {
            _orderItemService = orderItemService;
            _orderService = orderService;
        }

        
        [HttpGet("GetAll")]
        public IDataResult<List<OrderItem>> GetAll()
        {
            return _orderItemService.GetAll();
        }

        
        [HttpGet("GetAllByUser")]
        public IDataResult<List<OrderItem>> GetAllByUser()
        {
            var userId = GetUserIdFromClaims();

           
            var userOrders = _orderService.GetAll(o => o.UserId == userId).Data;

            if (userOrders == null || userOrders.Count == 0)
                return new SuccessDataResult<List<OrderItem>>(new List<OrderItem>(), "Sipariş bulunamadı");

            var orderIds = userOrders.ConvertAll(o => o.Id);

            
            var orderItems = _orderItemService.GetAll(oi => orderIds.Contains(oi.OrderId)).Data;

            return new SuccessDataResult<List<OrderItem>>(orderItems, "Sipariş ürünleri getirildi");
        }

       
        [HttpGet("GetById")]
        public IDataResult<OrderItem> GetById(Guid id)
        {
            var userId = GetUserIdFromClaims();

            var orderItemResult = _orderItemService.GetById(id);
            var orderItem = orderItemResult.Data;

            if (orderItem == null)
                return new ErrorDataResult<OrderItem>("Sipariş ürünü bulunamadı");

            var order = _orderService.GetById(orderItem.OrderId).Data;

            if (order == null || order.UserId != userId)
                return new ErrorDataResult<OrderItem>("Yetkiniz olmayan sipariş ürünü");

            return orderItemResult;
        }

        [HttpPost("Add")]
        public async Task<IResult> Add(OrderItem entity)
        {
            var userId = GetUserIdFromClaims();

            var order = _orderService.GetById(entity.OrderId).Data;
            if (order == null || order.UserId != userId)
                return new ErrorResult("Yetkiniz olmayan sipariş için ürün ekleyemezsiniz");

            return await _orderItemService.Add(entity);
        }

    
        [HttpPost("Update")]
        public async Task<IResult> Update(OrderItem entity)
        {
            return await _orderItemService.Update(entity);
        }

        [HttpPost("Delete")]
        public async Task<IResult> Delete(Guid id)
        {
            var userId = GetUserIdFromClaims();

            var orderItemResult = _orderItemService.GetById(id);
            var orderItem = orderItemResult.Data;

            if (orderItem == null)
                return new ErrorResult("Sipariş ürünü bulunamadı");

            var order = _orderService.GetById(orderItem.OrderId).Data;
            if (order == null || order.UserId != userId)
                return new ErrorResult("Yetkiniz olmayan sipariş ürününü silemezsiniz");

            return await _orderItemService.Delete(id);
        }

        private Guid GetUserIdFromClaims()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
                throw new UnauthorizedAccessException("Kullanıcı ID alınamadı.");
            return userId;
        }
    }
}
