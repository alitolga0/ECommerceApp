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
    public class CartItemService : ICartItemService
    {
        private readonly IBaseRepository<CartItem> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CartItemService(IBaseRepository<CartItem> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(CartItem entity)
        {
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("kayıt başarıyla tamamlandı");
        }

        public async Task<IResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("başarıyla silindi");
        }

        public IDataResult<List<CartItem>> GetAll(Expression<Func<CartItem, bool>> filter = null)
        {
            var data = _repository.GetAll(filter);
            return new SuccessDataResult<List<CartItem>>(data, "başarıyla listelendi");
        }


        public IDataResult<CartItem> GetById(Guid id)
        {
            var data= _repository.Get(x=>x.Id==id);
            return new SuccessDataResult<CartItem>(data,"başarıyla listlendi");
        }

        public async Task<IResult> Update(CartItem entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("başarıyla güncellendi");
        }
    }
}
