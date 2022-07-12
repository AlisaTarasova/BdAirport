using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BdAirport
{
    public class MiddlewareLogger
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public MiddlewareLogger(RequestDelegate rd, ILogger<MiddlewareLogger> l)
        {
            next = rd;
            logger = l;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            logger.LogInformation(context.Request.Path);
            await next.Invoke(context);
        }
    }
}
