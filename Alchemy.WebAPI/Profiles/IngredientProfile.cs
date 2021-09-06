using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Alchemy.DataModel.Entities.Ingredient, Ingredient>()
                .ForMember(dst => dst.Dlc, conf => conf.MapFrom(src => src.Dlc != null ? src.Dlc.Name : null));
            CreateMap<Alchemy.DataModel.Entities.Ingredient, IngredientLimited>();
        }
    }
}
