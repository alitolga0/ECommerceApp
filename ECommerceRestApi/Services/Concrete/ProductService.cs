using ECommerceRestApi.Core.Repository;
using ECommerceRestApi.Core.Utilities.Result;
using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;
using System.Linq.Expressions;

namespace ECommerceRestApi.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IBaseRepository<Product> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(Product entity)
        {
           await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("ürün başarıyla eklendi");
        }

        public async Task<IResult> Delete(Guid id)
        {
           await _repository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("ürün başarıyla silindi");
        }

        public IDataResult<List<Product>> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            var data = _repository.GetAll(filter);
            return new SuccessDataResult<List<Product>>(data,"ürünler başarıyla listelendi");
        }

        public IDataResult<Product> GetById(Guid id)
        {
            var data = _repository.Get(x=>x.Id == id);
            return new SuccessDataResult<Product>(data, "Ürün getirildi.");
        }

        public async Task<IResult> Update(Product entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("ürün başarıyla güncellendi");
        }
    }
}
