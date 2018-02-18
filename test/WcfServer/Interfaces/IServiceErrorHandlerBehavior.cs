using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WcfServer
{
    public interface IServiceErrorHandlerBehavior : IErrorHandler, IServiceBehavior
    {
    }
}