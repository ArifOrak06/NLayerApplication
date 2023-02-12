using NLayer.Core.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs.CategoryDtos
{
    // Tek kategoriye bağlı productların dönüleceği bir DataTransferObject
    public class CategoryWithProductDto : CategoryDto
    {
        public List<ProductDto> Products { get; set; }
    }
}
