using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class MixProfile : Profile
{
    public MixProfile()
    {
        CreateMap<BusinessLogic.Mix, Mix>();
        CreateMap<DataModel.Entities.Effect, EffectLimited>();
        CreateMap<DataModel.Entities.Ingredient, IngredientLimited>();
    }
}
