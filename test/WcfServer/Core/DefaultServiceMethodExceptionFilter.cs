using System;
using System.Reflection;

namespace WcfServer
{
    public class DefaultServiceMethodExceptionFilter : IServiceMethodExceptionFilter
    {
        public void Invoke(MethodInfo targetMethod, Exception exception)
        {
            return;
        }
    }
}
