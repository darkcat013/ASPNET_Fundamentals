using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Query["key"];
            if(string.IsNullOrEmpty(token))
            {
                await context.Response.WriteAsync($"No token");
            }
            else
            {
                await context.Response.WriteAsync($"Key = {token}");
            }
        }
    }
}
