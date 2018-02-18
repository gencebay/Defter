using Defter.SharedLibrary;
using Microsoft.AspNetCore.Mvc;
using NetCoreStack.Contracts;
using NetCoreStack.Mvc.Extensions;
using System.Collections.Generic;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetMessages(CollectionRequest request)
        {
            return Json(new List<DefterGenericMessage> { }.ToCollectionResult(request));
        }
    }
}