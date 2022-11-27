using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.RegularExpressions;

namespace t2itexp.Configure
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public static class ConfigureServicesBase
    {
        /// <summary>
        /// Configure Base Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddRouting();
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddOptions();
            services.AddLocalization();
            services.AddHttpContextAccessor();
            services.AddResponseCaching();
        }
    }
}