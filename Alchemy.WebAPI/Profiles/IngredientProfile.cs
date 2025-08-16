using Alchemy.DataModel.Entities;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<Ingredient, Models.Ingredient>()
            .ForMember(dst => dst.Dlc, conf => conf.MapFrom(src => src.Dlc != null ? src.Dlc.Name : null));
    }
}
