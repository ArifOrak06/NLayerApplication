using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Core.Entities.Abstractions;
using NLayer.Core.Services;

namespace NLayer.WebAPI.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        // NotFoundFilter bütün entityler için kullanılabilmesi için Generic yapılmıştır.
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); // ActionArguments = method'a parametre olarak client'tan gelen değerler "id"
            if (idValue == null)
            {
                await next.Invoke();
                return;
            }
            var id = (int)idValue; 
            // business'da yapılan null Check !
            var anyEntity = await _service.AnyAsnyc(x => x.Id == id); 
            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            // Data null ise hata mesajımızı dönelim. 
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) bulunamadı."));
        }
    }
}
