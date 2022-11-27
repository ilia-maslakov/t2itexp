using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace t2itexp.Configure
{
    /// <summary>
    /// Swagger configuration
    /// </summary>
    public static class ConfigureServicesSwagger
    {
        private const string AppTitle = "Test task API";
        private static readonly string AppVersion = "1.0";
        private const string SwaggerConfig = "/swagger/v1/swagger.json";
        private const string SwaggerUrl = "api/manual";

        /// <summary>
        /// ConfigureServices Swagger services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppTitle,
                    Version = AppVersion,
                    Description = "Test task example"
                });
                options.ResolveConflictingActions(x => x.First());
            });
        }

        /// <summary>
        /// Set up some properties for swagger UI
        /// </summary>
        /// <param name="settings"></param>
        public static void SwaggerSettings(SwaggerUIOptions settings)
        {
            settings.SwaggerEndpoint(SwaggerConfig, $"{AppTitle} v.{AppVersion}");
            settings.RoutePrefix = SwaggerUrl;
            settings.HeadContent = AppVersion;
            settings.DocumentTitle = $"{AppTitle}";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DefaultModelsExpandDepth(0);
            settings.DocExpansion(DocExpansion.None);
        }
    }
}
