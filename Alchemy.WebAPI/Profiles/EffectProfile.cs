using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class EffectProfile : Profile
{
    public EffectProfile()
    {
        CreateMap<DataModel.Entities.Effect, Models.Effect>();
        CreateMap<DataModel.Entities.Effect, EffectLimited>();
    }
}
