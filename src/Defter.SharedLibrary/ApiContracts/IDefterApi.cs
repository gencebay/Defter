using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    [ApiRoute("api/[controller]", "Main")]
    public interface IDefterApi : IApiContract
    {
        Task<CollectionResult<DefterGenericMessage>> Sorgu(CollectionRequest request);

        [HttpPostMarker]
        Task<ApiResult> Yaz(DefterGenericMessage model);
    }
}