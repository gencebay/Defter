using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    internal interface IBackgroundTask
    {
        void Invoke(TaskContext context);
    }
}
