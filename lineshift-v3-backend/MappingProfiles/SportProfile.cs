using AutoMapper;
using lineshift_v3_backend.Dtos.Sport;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.MappingProfiles
{
    public class SportProfile : Profile
    {
        public SportProfile()
        {
            CreateMap<Sport, SportDto>();
            CreateMap<CreateSportDto, Sport>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTimeOffset.UtcNow));
        }
    }
}
