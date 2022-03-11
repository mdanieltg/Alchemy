using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class IngredientProfile : Profile
{
    public IngredientProfile()
    {
        CreateMap<DataModel.Entities.Ingredient, Models.Ingredient>()
            .ForMember(dst => dst.Dlc, conf => conf.MapFrom(src => src.Dlc != null ? src.Dlc.Name : null));
        CreateMap<DataModel.Entities.Ingredient, IngredientLimited>();
    }
}
