﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Easy.Commerce.Services.Web.Api
{
    /// <summary>
    /// Partial startup class to API configuration.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// API service collection configuration.
        /// </summary>
        /// <param name="services">Service collection to be configured.</param>
        private void ConfigureApi(IServiceCollection services)
        {
            services.AddRouting(opt => opt.LowercaseUrls = true);

            services
                .AddMvc(options =>
                {
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());

                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();

                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                });

            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(new DateTime(2018, 9, 10));
            });
        }

        /// <summary>
        /// API application builder configuration.
        /// </summary>
        /// <param name="app">Application builder to be configured.</param>
        private void ConfigureApi(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "api/{version}/{controller}/{id?}");
            });
        }
    }
}
