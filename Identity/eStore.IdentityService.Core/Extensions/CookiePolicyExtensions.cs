using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace eStore.IdentityService.Core.Extensions
{
    public static class CookiePolicyExtensions
    {
        public static void ConfigureCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = (SameSiteMode)(-1);
                options.OnAppendCookie = cookieContext => OnSameCheckSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext => OnSameCheckSite(cookieContext.Context, cookieContext.CookieOptions);
            });
        }

        static void OnSameCheckSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite != SameSiteMode.None && options.SameSite != SameSiteMode.Unspecified) return;
            var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
            if (DisallowsSameSiteAgentNone(userAgent))
                options.SameSite = (SameSiteMode)(-1);
        }

        static bool DisallowsSameSiteAgentNone(string userAgent)
        {
            if (userAgent.Contains("CPU iPhone OS 12")
                || userAgent.Contains("iPad; CPU OS 12"))
                return true;


            if (userAgent.Contains("Safari")
                && userAgent.Contains("Macintosh; Intel Mac OS X 10_14")
                && userAgent.Contains("Version/"))
                return true;

            if (userAgent.Contains("Chrome")) return true;

            return false;
        }
    }
}
