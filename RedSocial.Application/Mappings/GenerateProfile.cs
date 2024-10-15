

using AutoMapper;
using RedSocial.Application.ViewModel.Users;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Mappings
{
    public class GenerateProfile : Profile
    {
        public GenerateProfile()
        {
            CreateMap<User, UserLoginViewModel>().ReverseMap();
            CreateMap<User, UserRegisterViewModel>()
                .ForMember(password => password.Password, ignorar => ignorar.Ignore())
                .ForMember(confirmedPassword => confirmedPassword.ConfirmPassword, ignorar => ignorar.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture)); ;

        }
    }
}
