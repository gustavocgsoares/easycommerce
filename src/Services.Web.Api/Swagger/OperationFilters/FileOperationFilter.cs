using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Easy.Commerce.Services.Web.Api.Swagger.OperationFilters
{
    /// <summary>
    /// File operation filter.
    /// </summary>
    public class FileOperationFilter : IOperationFilter
    {
        /// <summary>
        /// Aplly examples.
        /// </summary>
        /// <param name="operation">See <see cref="Operation"/>.</param>
        /// <param name="context">See <see cref="OperationFilterContext"/>.</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var anyFormData = context.ApiDescription.ParameterDescriptions
                .Any(x => x.ModelMetadata.ContainerType == typeof(IFormFile));

            if (anyFormData)
            {
                var pathParameters = operation.Parameters
                    .Where(par => par.In.Equals("path", StringComparison.CurrentCultureIgnoreCase));

                if (pathParameters != null && pathParameters.Count() > 0)
                {
                    operation.Parameters = pathParameters.ToList();
                }
                else
                {
                    operation.Parameters.Clear();
                }

                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "file", // must match parameter name from controller method
                    In = "formData",
                    Description = "File to upload.",
                    Required = true,
                    Type = "file",
                });

                operation.Consumes.Add("application/form-data");
            }
        }
    }
}
