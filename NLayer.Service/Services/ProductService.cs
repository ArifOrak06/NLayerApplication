using AutoMapper;
using NLayer.Core.DTOs.ProductDtos;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Core.Entities.Concrete;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IRepository<Product> repository, IProductRepository productRepository, IMapper mapper) : base(unitOfWork, repository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<CustomResponseDto<List<ProductWithCategoriesDto>>> GetProductsWithCategories()
        {
            var productsWithCategories = await _productRepository.GetProductsWithCategory();
            var productsWithCategoriesDto = _mapper.Map<List<ProductWithCategoriesDto>>(productsWithCategories);
            return CustomResponseDto<List<ProductWithCategoriesDto>>.Success(200,productsWithCategoriesDto);
        }
    }
}
