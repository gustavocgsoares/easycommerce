using System.Diagnostics.Contracts;
using Easy.Commerce.Data.Repositories.Corporate;
using Easy.Commerce.Domain.Interfaces.Repositories;
using Easy.Commerce.Domain.Interfaces.Services;
using Easy.Commerce.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Easy.Commerce.Services.Web.Api
{
    /// <summary>
    /// Partial startup class to main configuration.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Hosting environment.
        /// </summary>
        private IHostingEnvironment hostingEnv;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">Hosting environment to specific configuration, if exists.</param>
        public Startup(IHostingEnvironment env)
        {
            hostingEnv = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(true);
            }

            Configuration = builder.Build();
        }

        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// Main service collection configuration.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBasketService, BasketService>();
            services.AddTransient<IBasketRepository, BasketRepository>();

            services.Configure<CrossCutting.ConnectionStrings>(
                Configuration.GetSection(CrossCutting.ConnectionStrings.Section));

            ConfigureLocalization(services);
            ConfigureLogger(services);
            ConfigureCors(services);
            ConfigureSwagger(services);
            ConfigureMapper(services);
            ConfigureApi(services);
        }

        /// <summary>
        /// Main application builder configuration.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        /// <param name="env">Hosting environment to specific configuration, if exists.</param>
        /// <param name="loggerFactory">Logger factory to be configured.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Contract.Requires(env != null);

            app.UseStaticFiles();

            ConfigureLocalization(app);
            ConfigureLogger(app, loggerFactory);
            ConfigureCors(app);
            ConfigureSwagger(app);
            ConfigureApi(app);
        }
    }
}
