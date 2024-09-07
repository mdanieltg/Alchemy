using Alchemy.Domain.Entities;
using Alchemy.Domain.Models;
using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<DownloadableContent, DownloadableContentDto>();
        CreateMap<Effect, EffectDto>();
        CreateMap<Effect, EffectLimited>();
        CreateMap<Ingredient, IngredientDto>()
            .ForMember(dst => dst.Dlc, conf => conf.MapFrom(src => src.Dlc != null ? src.Dlc.Name : null));
        CreateMap<Ingredient, IngredientLimited>();
        CreateMap<Mix, MixDto>();
    }
}
