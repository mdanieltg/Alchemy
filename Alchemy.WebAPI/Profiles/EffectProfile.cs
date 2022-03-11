using Alchemy.DataModel.Entities;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class EffectProfile : Profile
{
    public EffectProfile()
    {
        CreateMap<Effect, Models.Effect>();
    }
}
