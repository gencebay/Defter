using System.Reflection;

namespace WcfServer
{
    public class DefaultServiceLogger : IServiceLogger
    {
        public void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return)
        {
            
        }
    }
}