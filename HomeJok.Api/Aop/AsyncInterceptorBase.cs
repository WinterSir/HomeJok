using Castle.DynamicProxy;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace HomeJok.Api
{
    public abstract class AsyncInterceptorBase : IInterceptor
    {
        public AsyncInterceptorBase()
        {
        }

        public void Intercept(IInvocation invocation)
        {
            BeforeProceed(invocation);
            invocation.Proceed();
            if (IsAsyncMethod(invocation.MethodInvocationTarget))
            {
                InterceptAsync((dynamic)invocation.ReturnValue, invocation);
            }
            else
            {
                AfterProceedSync(invocation);
            }
        }

        private bool CheckMethodReturnTypeIsTaskType(MethodInfo method)
        {
            var methodReturnType = method.ReturnType;
            if (methodReturnType.IsGenericType)
            {
                if (methodReturnType.GetGenericTypeDefinition() == typeof(Task<>) ||
                    methodReturnType.GetGenericTypeDefinition() == typeof(ValueTask<>))
                    return true;
            }
            else
            {
                if (methodReturnType == typeof(Task) ||
                    methodReturnType == typeof(ValueTask))
                    return true;
            }
            return false;
        }

        private bool IsAsyncMethod(MethodInfo method)
        {
            bool isDefAsync = Attribute.IsDefined(method, typeof(AsyncStateMachineAttribute), false);
            bool isTaskType = CheckMethodReturnTypeIsTaskType(method);
            bool isAsync = isDefAsync && isTaskType;

            return isAsync;
        }

        private async Task InterceptAsync(Task task, IInvocation invocation)
        {
            await task.ConfigureAwait(false);
            AfterProceedAsync(invocation, false);
        }

        private async Task<TResult> InterceptAsync<TResult>(Task<TResult> task, IInvocation invocation)
        {
            TResult ProceedAsyncResult = await task.ConfigureAwait(false);
            invocation.ReturnValue = ProceedAsyncResult;
            AfterProceedAsync(invocation, true);
            return ProceedAsyncResult;
        }

        private async ValueTask InterceptAsync(ValueTask task, IInvocation invocation)
        {
            await task.ConfigureAwait(false);
            AfterProceedAsync(invocation, false);
        }

        private async ValueTask<TResult> InterceptAsync<TResult>(ValueTask<TResult> task, IInvocation invocation)
        {
            TResult ProceedAsyncResult = await task.ConfigureAwait(false);
            invocation.ReturnValue = ProceedAsyncResult;
            AfterProceedAsync(invocation, true);
            return ProceedAsyncResult;
        }

        protected virtual void BeforeProceed(IInvocation invocation) { }
        protected virtual void AfterProceedSync(IInvocation invocation) { }
        protected virtual void AfterProceedAsync(IInvocation invocation, bool hasAsynResult) { }
    }
}
