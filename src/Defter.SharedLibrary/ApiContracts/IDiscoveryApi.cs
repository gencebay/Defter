using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    [ApiRoute("api/[controller]", "main")]
    public interface IDiscoveryApi : IApiContract
    {
        Task<ApiResult> GetConnections();

        [HttpPostMarker]
        Task<ApiResult> BroadcastAsync(BroadcastContext context);
    }
}