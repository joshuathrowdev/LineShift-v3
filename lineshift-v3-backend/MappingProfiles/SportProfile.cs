using AutoMapper;
using lineshift_v3_backend.Dtos;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.MappingProfiles
{
    public class SportProfile : Profile
    {
        public SportProfile()
        {
            CreateMap<Sport, SportDto>();
        }
    }
}
