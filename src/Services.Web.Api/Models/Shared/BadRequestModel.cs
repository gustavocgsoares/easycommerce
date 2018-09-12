using System.Collections.Generic;
using Newtonsoft.Json;

namespace Easy.Commerce.Services.Web.Api.Models.Shared
{
    /// <summary>
    /// Bad request model.
    /// </summary>
    public class BadRequestModel
    {
        /// <summary>
        /// Error code.
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// Error message.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Error validations.
        /// </summary>
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public virtual List<string> Validations { get; set; }
    }
}