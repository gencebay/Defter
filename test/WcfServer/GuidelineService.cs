using System;
using Wcf.Contracts;

namespace WcfServer.Hosting
{
    public class GuidelineService : ServiceBase, IGuidelineService
    {
        public SampleDependency Dependency { get; }
        public GuidelineService(SampleDependency dependency)
        {
            Dependency = dependency;
        }

        public ServiceResult<CompositeType> RefTypeParameter(CompositeType model)
        {
            return new ServiceResult<CompositeType>(new CompositeType
            {
                BoolValue = true,
                StringValue = $"Echo from server {DateTime.Now.ToString()}",
            });
        }

        public ServiceResult<int> ReturnPrimitive()
        {
            return new ServiceResult<int>(10);
        }

        public ServiceResult ThrowContractRuleException(long requiredParam)
        {
            throw new InvalidOperationException($"{nameof(InvalidOperationException)} - param: {requiredParam}");
        }

        public ServiceResult ThrowException()
        {
            throw new ArgumentNullException(nameof(ThrowException));
        }

        public ServiceResult<int> LoggedServiceMethod(CompositeType parameter)
        {
            return new ServiceResult<int>(10);
        }
    }
}