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
    public class ProductSeedData : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product[]
            {
                new(){Id=1,Name="Kırmızı Kalem",Stock=200,Price=56,CategoryId=1,CreatedDate=DateTime.Now},
                new(){Id=2,Name="Mavi Kalem",Stock=200,Price=56,CategoryId=1,CreatedDate=DateTime.Now},
                new(){Id=3,Name="Nefes Nefese Ayşe Kulin",Stock=100,Price=25,CategoryId=2,CreatedDate=DateTime.Now},
                new(){Id=4,Name="60 Yapraklı Kare defter",Stock=400,Price=20,CategoryId=3,CreatedDate=DateTime.Now}
            });
        }
    }
}
