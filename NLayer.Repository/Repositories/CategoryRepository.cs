using Microsoft.EntityFrameworkCore;
using NLayer.Core.Entities.Concrete;
using NLayer.Core.Repositories;
using NLayer.Repository.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : Repository<Product>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context):base(context)
        {

        }
        public async Task<Category> GetCategoryByIdWithProducts(int id)
        {
            return  await _context.Categories.Include(c => c.Products).Where(c=> c.Id == id).SingleOrDefaultAsync(); 
        }
    }
}
