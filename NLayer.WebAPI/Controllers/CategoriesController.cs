using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.CategoryDtos;
using NLayer.Core.Services;
using NLayer.WebAPI.Filters;

namespace NLayer.WebAPI.Controllers
{

 
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        // GET localhost/api/categories/GetCategoryByIdWidthProducts/1
        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetCategoryByIdWidthProducts(int categoryId)
        {
            var response = await _categoryService.GetCategoryByIdWithProductsAsync(categoryId);

            return response != null ? CreateActionResult<CategoryWithProductDto>(response) : NotFound();


        }
    }
}
