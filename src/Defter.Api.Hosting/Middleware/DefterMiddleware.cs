using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading.Tasks;

namespace Defter.Api.Hosting.Middleware
{
    public class DefterMiddleware
    {
        private readonly RequestDelegate _next;

        public DefterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestCulture = context.Features.Get<IRequestCultureFeature>();
            await _next(context);
        }
    }
}
