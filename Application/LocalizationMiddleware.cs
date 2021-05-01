using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var userLangs = context.Request.Headers["Accept-Language"].ToString();

            var isArabic = string.IsNullOrEmpty(userLangs) || userLangs.Contains("ar") || !userLangs.Contains("en");

            var culture = new CultureInfo(isArabic ? "ar-ae" : "en");

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            await next(context);
        }
    }
}
