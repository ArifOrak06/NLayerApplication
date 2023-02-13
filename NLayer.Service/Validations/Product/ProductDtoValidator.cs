using FluentValidation;
using NLayer.Core.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Validations.Product
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} null olamaz").NotEmpty().WithMessage("{PropertyName} alanı zorunlu bir alandır.");
            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı 0 rakamından büyük bir değer olmalı");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı 0 rakamından büyük bir değer olmalı");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} alanı 0 rakamından büyük bir değer olmalı");
        }
    }
}
