using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Contracts;
using System.Threading.Tasks;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDefterApi _defterApi;

        public HomeController(IDefterApi defterApi)
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