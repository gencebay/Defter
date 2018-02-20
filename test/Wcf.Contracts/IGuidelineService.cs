using NetCoreStack.Contracts;
using System.ServiceModel;

namespace Wcf.Contracts
{
    [ServiceContract]
    public interface IGuidelineService
    {
        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        ServiceResult SomeQuery(string value);
    }
}