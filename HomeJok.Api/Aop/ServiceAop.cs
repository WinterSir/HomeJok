using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeJok.Api
{
    public class ServiceAop : AsyncInterceptorBase
    {
        private readonly ILogger<ServiceAop> _logger;
        public ServiceAop(ILogger<ServiceAop> logger)
        {
            _logger = logger;
        }
        protected override void BeforeProceed(IInvocation invocation)
        {
            _logger.LogInformation($"ServiceAop调用方法：{invocation.Method.Name}，参数：{JsonConvert.SerializeObject(invocation.Arguments) }");
        }

        protected override void AfterProceedSync(IInvocation invocation)
        {
            _logger.LogInformation($"ServiceAop同步返回结果：{JsonConvert.SerializeObject(invocation.ReturnValue)}");
        }

        protected override void AfterProceedAsync(IInvocation invocation, bool hasAsynResult)
        {
            _logger.LogInformation($"ServiceAop异步返回结果：{JsonConvert.SerializeObject(invocation.ReturnValue)}");
        }
    }
}
