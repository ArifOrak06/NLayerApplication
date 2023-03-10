using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs.ProductDtos;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Core.Entities.Concrete;
using NLayer.Core.Services;
using NLayer.WebAPI.Filters;

namespace NLayer.WebAPI.Controllers
{
  
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IProductService _service;

        public ProductsController(IService<Product> service, IMapper mapper, IProductService productService)
        {
        
            _mapper = mapper;
            _service = productService;
        }
        // GET localhost:7255/api/products/GetProductsWithCategories
        [HttpGet("GetProductsWithCategories")]
        public async Task<IActionResult> GetProductsWithCategories()
        {
            var response = await _service.GetProductsWithCategories();
            return CreateActionResult<List<ProductWithCategoriesDto>>(response);
        }

        // GET localhost:7255/api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDto = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
            // result statusCode'una göre response dönecek CreateActionResult class'ı sayesinde
            return CreateActionResult<List<ProductDto>>(CustomResponseDto<List<ProductDto>>.Success(200, productsDto));
        }
        [ServiceFilter(typeof(NotFoundFilter<>))]
        // GET localhost:7255/api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(200, productDto));
        }

        // POST localhost:7255/api/products
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto dto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(dto));
            var productDto = _mapper.Map<ProductDto>(product);
            return CreateActionResult<ProductDto>(CustomResponseDto<ProductDto>.Success(201, productDto));
        }
        // PUT localhost:7255/api/products
        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto dto)
        {
            var updatedProduct = await _service.UpdateAsync(_mapper.Map<Product>(dto));
           
            return CreateActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return CreateActionResult<NoContentDto>(CustomResponseDto<NoContentDto>.Success(204));

        }

    }
}
