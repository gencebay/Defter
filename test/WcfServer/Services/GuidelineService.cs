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

        public ServiceResult SomeQuery(string value)
        {
            return new ServiceResult<CompositeType>(new CompositeType
            {
                BoolValue = true,
                StringValue = $"Echo from server {DateTime.Now.ToString()}",
            });
        }
    }
}