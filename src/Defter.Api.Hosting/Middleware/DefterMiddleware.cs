using Microsoft.AspNetCore.Http;
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
            await _next(context);
        }
    }
}
