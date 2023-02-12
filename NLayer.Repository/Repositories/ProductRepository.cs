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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context):base(context)
        {

        }
        public async Task<List<Product>> GetProductsWithCategory()
        {
           return await _context.Products.Include(p=> p.Category).ToListAsync();
        }
    }
}
