using ECommerceRestApi.Core.Repository;
using ECommerceRestApi.Core.Utilities.Result;
using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
namespace ECommerceRestApi.Services.Concrete
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBaseRepository<OrderItem> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IBaseRepository<OrderItem> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(OrderItem entity)
        {
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("başarıyla eklendi");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("başarıyla silindi");
                                
        }

        public IDataResult<List<OrderItem>> GetAll(Expression<Func<OrderItem, bool>> filter = null)
        {
            var data= _repository.GetAll(filter);
            return new SuccessDataResult<List<OrderItem>>(data,"başarıyla listelendi");
        }

        public IDataResult<OrderItem> GetById(Guid id)
        {
            var data = _repository.Get(x=>x.Id == id);
            return new SuccessDataResult<OrderItem>(data,"başarıyla getirildi");
        }

        public async Task<IResult> Update(OrderItem entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("başarıyla güncellendi");
        }
    }
}
