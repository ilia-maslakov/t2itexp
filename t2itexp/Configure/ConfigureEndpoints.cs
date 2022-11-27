using Microsoft.AspNetCore.Builder;

namespace t2itexp.Configure
{
    /// <summary>
    /// Configure Endpoints
    /// </summary>
    public static class ConfigureEndpoints
    {
        /// <summary>
        /// Configure Routing
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
