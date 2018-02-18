using System;
using System.Reflection;

namespace WcfServer
{
    public interface IServiceMethodExceptionFilter
    {
        void Invoke(MethodInfo targetMethod, Exception exception);
    }
}