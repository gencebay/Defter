using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IDefterApi _defterApi;

        public ConfigController(IDefterApi defterApi)
        {
            _defterApi = defterApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetMessages(CollectionRequest request)
        {
            return Json(await _defterApi.Sorgu(request));
        }
    }
}