﻿using System.Reflection;

namespace WcfServer
{
    public interface IServiceLogger
    {
        void Invoke(string callerId, string serviceName, MethodInfo targetMethod, object args, object @return);
    }
}
