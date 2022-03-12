using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeJok.Api
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                string msg = context.Exception.Message;
                context.Result = new ContentResult
                {
                    Content = msg,
                    StatusCode = 200,
                    ContentType = "application/json"
                };
                _logger.LogError(msg);
            }
            context.ExceptionHandled = true; //异常已处理了
            return Task.CompletedTask;
        }
    }
}
