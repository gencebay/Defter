using System.Threading.Tasks;

namespace Defter.Api.Hosting
{
    public interface IJob
    {
        string Id { get; }
        Task InvokeAsync(TaskContext context);
    }
}