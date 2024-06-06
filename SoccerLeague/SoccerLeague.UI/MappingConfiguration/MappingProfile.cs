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

            CreateMap<League, LeagueRequestModel>()
                .ReverseMap();

            CreateMap<League, LeagueResponseModel>()
                .ReverseMap();

            CreateMap<Team, TeamRequestModel>()
                .ReverseMap();

            CreateMap<Team, TeamResponseModel>()
                .ReverseMap();

            CreateMap<Player, PlayerRequestModel>()
                .ReverseMap();

            CreateMap<Player, PlayerResponseModel>()
                .ReverseMap();

            CreateMap<Match, MatchRequestModel>()
                .ReverseMap();

            CreateMap<Match, MatchResponseModel>()
                .ReverseMap();
        }
    }
}