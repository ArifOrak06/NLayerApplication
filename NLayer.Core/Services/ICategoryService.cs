using NLayer.Core.DTOs.CategoryDtos;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Services
{
    public interface ICategoryService: IService<Category>
    {
        Task<CustomResponseDto<CategoryWithProductDto>> GetCategoryByIdWithProductsAsync(int id);
    }
}
