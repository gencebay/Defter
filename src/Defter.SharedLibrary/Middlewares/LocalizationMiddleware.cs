using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Defter.SharedLibrary
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            if (!context.Request.Cookies.ContainsKey(CookieRequestCultureProvider.DefaultCookieName))
            {
                context.Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(HostingFactory.DefaultCulture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(7) });
            }

            return _next(context);
        }
    }
}