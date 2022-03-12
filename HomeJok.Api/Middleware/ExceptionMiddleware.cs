using HomeJok.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeJok.Api
{
    public class ExceptionMiddleware
    {
        /// <summary>
        /// 委托
        /// </summary>
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                var exception = new
                {
                    Content = msg,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                _logger.LogError(msg);
                await context.Response.WriteAsync(JsonConvert.SerializeObject(exception));
            }
        }
    }
}
