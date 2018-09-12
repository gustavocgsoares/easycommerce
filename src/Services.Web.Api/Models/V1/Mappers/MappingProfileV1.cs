using Easy.Commerce.Domain.Entities;
using Easy.Commerce.Services.Web.Api.Models.Shared;
using Easy.Commerce.Services.Web.Api.Models.V1;

namespace Easy.Commerce.Application.Model.Contexts.V1.Mappers
{
    /// <summary>
    /// Mapping v1 profiles.
    /// </summary>
    public class MappingProfileV1 : MappingProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfileV1"/> class.
        /// </summary>
        public MappingProfileV1()
        {
            CreateMap(typeof(Basket), typeof(BasketModel)).ReverseMap();
            CreateMap(typeof(Product), typeof(ProductModel)).ReverseMap();
        }
    }
}