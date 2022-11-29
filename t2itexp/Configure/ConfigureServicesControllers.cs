using Microsoft.Extensions.DependencyInjection;

namespace t2itexp.Configure
{
    /// <summary>
    /// Configure controllers
    /// </summary>
    public static class ConfigureServicesControllers
    {
        /// <summary>
        /// Configure controllers
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services)
        {
//            services.AddControllers();
            services.AddControllersWithViews();
        }
    }
}
