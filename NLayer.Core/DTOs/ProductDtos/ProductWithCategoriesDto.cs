using NLayer.Core.DTOs.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.ProductDtos
{
    public class ProductWithCategoriesDto : ProductDto
    {
        public CategoryDto  Category { get; set; }
    }
}
