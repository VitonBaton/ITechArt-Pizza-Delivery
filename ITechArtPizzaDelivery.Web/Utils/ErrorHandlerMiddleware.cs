using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ITechArtPizzaDelivery.Domain.Errors;
using Microsoft.AspNetCore.Http;

namespace ITechArtPizzaDelivery.Web.Utils
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    KeyNotFoundException => (int) HttpStatusCode.NotFound,
                    BadRequestException => (int) HttpStatusCode.BadRequest,
                    _ => (int) HttpStatusCode.InternalServerError
                };
                
                await response.WriteAsJsonAsync(new { message= error.Message });
            }
        }
    }
}