using ECommerceRestApi.Core.Repository;
using ECommerceRestApi.Core.Utilities.Result;
using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
namespace ECommerceRestApi.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBaseRepository<Order> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(Order entity)
        {
         
            // 1. OrderDate ve Status ayarla
            entity.OrderDate = DateTime.UtcNow;
            entity.Status = OrderStatus.Pending;

            // 2. Toplam tutarı hesapla
            entity.TotalAmount = entity.OrderItems.Sum(oi => oi.UnitPrice * oi.Quantity);

            // 3. Önce order ekle
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            // Burada EF Core OrderId'yi otomatik atar ve OrderItem'lar buna bağlanır

            return new SuccessResult("sipariş başarıyla eklendi");
        }

        public List<Order> GetAllByUserWithDetails(Guid userId)
        {

            var orders = _repository.GetAllWithNavigation(
                o => o.UserId == userId,
                "OrderItems",
                "OrderItems.Product",
                "OrderItems.Product.Category"
            );

            return orders;
        }


        public async Task<IResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("sipariş başarıyla silindi");
        }

        public IDataResult<List<Order>> GetAll(Expression<Func<Order, bool>> filter = null)
        {
            var data = _repository.GetAllWithNavigation(
          filter,
          "OrderItems",
          "OrderItems.Product"
      );
            return new SuccessDataResult<List<Order>>(data, "siparişler başarıyla getirildi");
        }

        public IDataResult<Order> GetById(Guid id)
        {
            var data = _repository.Get(x=> x.Id == id);
            return new SuccessDataResult<Order>(data, "Order getirildi.");
        }

        public async Task<IResult> Update(Order entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("sipariş başarıyla güncellendi");
        }
    }
}
