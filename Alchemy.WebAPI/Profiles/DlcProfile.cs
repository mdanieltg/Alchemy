using AutoMapper;
using Alchemy.DataModel.Entities;

namespace Alchemy.WebAPI.Profiles
{
    public class DlcProfile : Profile
    {
        public DlcProfile()
        {
            CreateMap<Dlc, Models.Dlc>();
        }
    }
}
