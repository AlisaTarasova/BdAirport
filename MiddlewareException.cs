using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BdAirport
{
    public class MiddlewareException
    {
        private readonly RequestDelegate next;

        public MiddlewareException(RequestDelegate rd)
        {
            next = rd;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessage(context, ex).ConfigureAwait(false);
            }
        }

        private Task HandleExceptionMessage(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string error;
            switch (statusCode)
            {
                case 400: error = $"Exception {exception.Message} bad request."; break;
                case 404: error = $"Exception {exception.Message} not found"; break;
                case 500: error = $"Exception {exception.Message} internal cerver error"; break;
                default: error = $"Exception {exception.Message} unknown error"; break;
            }
            var result = JsonSerializer.Serialize(new
            {
                StatusCode = statusCode,
                ErrorMessage = error
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
