using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    public class ProductFeatureSeedData : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature[]
            {
                new()
                {
                    Id = 1,
                    Color = "Kırmızı",
                    ProductId = 1,
                    Height = 10,
                    Width = 3
                },
                new()
                {
                    Id= 2,
                    Color ="Mavi",
                    ProductId = 2,
                    Height = 10,
                    Width = 2
                }
            });
        }
    }
}
