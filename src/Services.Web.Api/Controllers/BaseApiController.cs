using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Easy.Commerce.Services.Web.Api.Controllers
{
    /// <summary>
    /// Base api controller.
    /// </summary>
    [Controller]
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        /// API date format (YYYY-MM-DD).
        /// </summary>
        public const string ApiDateFormat = ":datetime:regex(\\d{4}-\\d{2}-\\d{2})";

        /// <summary>
        /// API date time format (YYYY-MM-DDTHH-mm | YYYY-MM-DDTHH-mm-SS).
        /// </summary>
        public const string ApiDateTimeFormat = ":datetime:regex(\\d{4}-\\d{2}-\\d{2}T(\\d{2}:\\d{2}:\\d{2}|\\d{2}:\\d{2})";

        /// <summary>
        /// See <see cref="IUrlHelperFactory"/>.
        /// </summary>
        private readonly IUrlHelperFactory urlHelperFactory;

        /// <summary>
        /// See <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApiController"/> class.
        /// </summary>
        /// <param name="mapper">See <see cref="IMapper"/>.</param>
        /// <param name="urlHelperFactory">See <see cref="IUrlHelperFactory"/>.</param>
        public BaseApiController(
            IMapper mapper,
            IUrlHelperFactory urlHelperFactory)
        {
            this.mapper = mapper;
            this.urlHelperFactory = urlHelperFactory;
        }
    }
}
