using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Controllers
{
    [Route("api/[controller]")]
    public class DefterController : ControllerBase, IDefterApi
    {
        [HttpPost(nameof(GenericCapture))]
        public async Task<ApiResult> GenericCapture([FromBody]DefterGenericMessage model)
        {
            if (ModelState.IsValid)
            {

            }

            await Task.CompletedTask;
            return new ApiResult();
        }
    }
}