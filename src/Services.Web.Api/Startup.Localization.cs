using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Easy.Commerce.Services.Web.Api
{
    /// <summary>
    /// Partial startup class to localization configuration.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Localization service collection configuration.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        private void ConfigureLocalization(IServiceCollection services)
        {
            services.AddLocalization(options => options.ResourcesPath = "Easy.Commerce.CrossCutting/Resources");

            services.Configure<RequestLocalizationOptions>(
            opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("pt-BR"),
                    new CultureInfo("en-US"),
                };

                //// Formatting numbers, dates, etc.
                opts.SupportedCultures = supportedCultures;
                //// UI strings that we have localized.
                opts.SupportedUICultures = supportedCultures;
                opts.DefaultRequestCulture = new RequestCulture("pt-BR");

                opts.RequestCultureProviders = new List<IRequestCultureProvider>
                {
                    new AcceptLanguageHeaderRequestCultureProvider(),
                };
            });
        }

        /// <summary>
        /// Localization application builder configuration.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        private void ConfigureLocalization(IApplicationBuilder app)
        {
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
        }
    }
}
