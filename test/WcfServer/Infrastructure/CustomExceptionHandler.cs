using System;
using System.ServiceModel.Dispatcher;

namespace WcfServer
{
    public class CustomExceptionHandler : ExceptionHandler
    {
        public override bool HandleException(Exception exception)
        {
            return true;
        }
    }
}