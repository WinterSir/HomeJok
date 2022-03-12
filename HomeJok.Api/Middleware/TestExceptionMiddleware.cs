using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeJok.Api
{
    public class TestExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public TestExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            throw new ArgumentException("TestExceptionMiddleware");
        }
    }
}
