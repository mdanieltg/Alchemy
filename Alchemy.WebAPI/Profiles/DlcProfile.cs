using Alchemy.DataModel.Entities;
using AutoMapper;

namespace Alchemy.WebAPI.Profiles;

public class DlcProfile : Profile
{
    public DlcProfile()
    {
        CreateMap<Dlc, Models.Dlc>();
    }
}
