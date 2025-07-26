using AutoMapper;
using lineshift_v3_backend.Dtos.GoverningBody;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.MappingProfiles
{
    public class GoverningBodyProfile : Profile
    {
        public GoverningBodyProfile()
        {
            CreateMap<GoverningBody, GoverningBodyDto>();
        }
    }
}
