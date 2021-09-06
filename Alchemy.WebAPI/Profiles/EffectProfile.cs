using Alchemy.WebAPI.Models;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles
{
    public class EffectProfile : Profile
    {
        public EffectProfile()
        {
            CreateMap<Alchemy.DataModel.Entities.Effect, Effect>();
            CreateMap<Alchemy.DataModel.Entities.Effect, EffectLimited>();
        }
    }
}
