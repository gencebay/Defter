using System;

namespace Defter.Api.Hosting.BackgroundJob
{
    public interface IProcessFactory
    {
        IJob Create(Type type);
    }
}