using AutoMapper;
using lineshift_v3_backend.Dtos.League;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.MappingProfiles
{
    public class LeagueProfile : Profile
    {
        public LeagueProfile()
        {
            CreateMap<League, LeagueDto>();
        }
    }
}
