using AutoMapper;
using SoccerLeague.Models.Data;
using SoccerLeague.Models.Request;
using SoccerLeague.Models.Response;

namespace SoccerLeague.UI.MappingConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Arena, ArenaRequestModel>()
                .ReverseMap();

            CreateMap<Arena, ArenaResponseModel>()
                .ReverseMap();
        }
    }
}