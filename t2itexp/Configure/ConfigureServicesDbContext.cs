using t2itexp.Data;
using t2itexp.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace t2itexp.Configure
{
    /// <summary>
    /// Configure DbContext
    /// </summary>
    public static class ConfigureServicesDbContext
    {
        /// <summary>
        /// Configure services
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            string consrt = configuration.GetConnectionString("DefaultConnection") ?? "";
            if (consrt == "")
            {
                Console.WriteLine("DefaultConnection is not set in appsettings.json");
            }
            services.AddDbContext<DBphoneContext>(o => o.UseNpgsql(consrt));
            services.AddScoped<UnitOfWork>();
            services.AddScoped<PhoneRepository>();
        }
    }
}
