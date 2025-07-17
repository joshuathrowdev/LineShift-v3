using AutoMapper;
using lineshift_v3_backend.Dtos.Identity;
using lineshift_v3_backend.Models;

namespace lineshift_v3_backend.MappingProfiles
{
    public class UserProfil : Profile
    {
       public UserProfil()
        {
            // Configure mapping from Application User to SessionUser(Dto)
            CreateMap<ApplicationUser, SessionUser>()
            // Example: If a property name is different or needs custom logic
            // .ForMember(dest => dest.SomeClientProperty, opt => opt.MapFrom(src => src.SomeInternalProperty))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            // Example: If you want to ignore a property from the source that exists in destination
            // .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Not needed if dest doesn't have it

            // Example: If ApplicationUser has FirstName and LastName, but UserDto needs a FullName
            // .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))

            // Example: Handling the IsDeleted property (if UserDto doesn't have it, it's ignored by default)
            // If UserDto had an 'IsActive' property, you could map it:
            // .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => !src.IsDeleted))
            ;
        }
    }
}
