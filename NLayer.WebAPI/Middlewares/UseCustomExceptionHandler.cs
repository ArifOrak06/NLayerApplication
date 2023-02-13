using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs.ResponseDto;
using NLayer.Service.Exceptions;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace NLayer.WebAPI.Middlewares
{
    public static class UseCustomExceptionHandler
    {
        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>(); // uygulamadan hata fırlatılmış ise alıyoruz.

                    var statusCode = exceptionFeature.Error switch // hataları kontrol ediyoruz. Server kaynaklı hatalar 500, client kaynaklı hatalar 400
                    {
                        ClientSideException => 400,
                        NotFoundException=> 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;
                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });
        }
    }
}
