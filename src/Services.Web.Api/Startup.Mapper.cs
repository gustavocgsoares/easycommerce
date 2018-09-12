using AutoMapper;
using Easy.Commerce.Application.Model.Contexts.V1.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Easy.Commerce.Services.Web.Api
{
    /// <summary>
    /// Partial startup class to Mapper configuration.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Mapper service collection configuration.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        public void ConfigureMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMissingTypeMaps = true;
                cfg.AddProfile(new MappingProfileV1());
            });
        }
    }
}
