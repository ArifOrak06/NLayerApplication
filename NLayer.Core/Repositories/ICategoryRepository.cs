using NLayer.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Product>
    {
        Task<Category> GetCategoryByIdWithProducts(int id);
    }
}
