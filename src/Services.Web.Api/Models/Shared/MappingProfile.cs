using AutoMapper;
using Easy.Commerce.Domain.Shared;

namespace Easy.Commerce.Services.Web.Api.Models.Shared
{
    /// <summary>
    /// Mapping profiles.
    /// </summary>
    public partial class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// </summary>
        public MappingProfile()
        {
            CreateMap(typeof(Entity<,>), typeof(BaseModel<>)).ReverseMap();
        }
    }
}