using ECommerceRestApi.Core.Repository;
using ECommerceRestApi.Core.Utilities.Result;
using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using System.Linq.Expressions;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;

namespace ECommerceRestApi.Service.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IBaseRepository<Category> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> Add(Category entity)
        {
            await _repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kategori başarıyla eklendi.");
        }

      
        public async Task<IResult> Delete(Guid id)
        {
            await _repository.Delete(id);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kategori başarıyla silindi.");
        }

        public IDataResult<List<Category>> GetAll(Expression<Func<Category, bool>> filter = null)
        {
            var data = _repository.GetAll(filter);
            return new SuccessDataResult<List<Category>>(data, "Kategoriler başarıyla listelendi.");
        }

        public IDataResult<Category> GetById(Guid id)
        {
            var data = _repository.Get(x => x.Id == id);
            return new SuccessDataResult<Category>(data, "Kategori başarıyla getirildi.");
        }
        public async Task<IResult> Update(Category entity)
        {
            await _repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult("Kategori başarıyla güncellendi.");
        }
    }
}
