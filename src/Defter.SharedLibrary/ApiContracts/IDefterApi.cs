using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    [ApiRoute("api/[controller]", "main")]
    public interface IDefterApi : IApiContract
    {
        [HttpPostMarker]
        Task<ApiResult> GenericCapture(DefterGenericMessage model);
    }
}