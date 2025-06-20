﻿using ECommerceRestApi.Models;
using ECommerceRestApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ECommerceRestApi.Core.Utilities.Result;
using IResult = ECommerceRestApi.Core.Utilities.Result.IResult;

namespace ECommerceRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpGet("GetAll")]
        public IDataResult<List<Category>> GetAll()
        {
            return _categoryService.GetAll();
        }

        [HttpGet("GetById")]
        public IDataResult<Category> GetById(Guid id)
        {
            return _categoryService.GetById(id);
        }

       
        [HttpPost("Add")]
        public async Task<IResult> Add(Category entity)
        {
            return await _categoryService.Add(entity);
        }

        [HttpPost("Update")]
        public async Task<IResult> Update(Category entity)
        {
            return await _categoryService.Update(entity);
        }

        [HttpPost("Delete")]
        public async Task<IResult> Delete(Guid id)
        {
            return await _categoryService.Delete(id);
        }
    }
}
