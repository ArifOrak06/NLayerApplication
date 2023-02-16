using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs.ProductDtos;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Core.Entities.Concrete;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching.Services
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            if(_memoryCache.TryGetValue(CacheProductKey,out _))
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public async Task<bool> AnyAsnyc(Expression<Func<Product, bool>> expression)
        {
            return await _repository.AnyAsnyc(expression);
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey));
        }

        public  Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault();
            if (product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) bulunamadı");
            }
            return Task.FromResult(product);
        }

        public  Task<CustomResponseDto<List<ProductWithCategoriesDto>>> GetProductsWithCategories()
        {
            // cacheleme yapılmadı.
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoriesDto = _mapper.Map<List<ProductWithCategoriesDto>>(products);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoriesDto>>.Success(200, productsWithCategoriesDto));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;


        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();

        }
        public async Task CacheAllProductsAsync()
        {
            _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
