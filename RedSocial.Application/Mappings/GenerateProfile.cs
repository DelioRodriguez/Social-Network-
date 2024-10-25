using AutoMapper;
using RedSocial.Application.ViewModel.Comments;
using RedSocial.Application.ViewModel.Posts;
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
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

            CreateMap<UserProfileEditViewModel, User>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());

            CreateMap<User, UserProfileEditViewModel>()
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore());

            
            CreateMap<Comment, CommentViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies)); 

            CreateMap<CommentReply, CommentReplyViewModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));

            CreateMap<CreateCommentReplyViewModel, CommentReply>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()); 

           
            CreateMap<Post, PostViewModel>()
                .ForMember(dest => dest.UserProfileImage, opt => opt.MapFrom(src => src.User.ProfilePicture))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<CreatePostViewModel, Post>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            CreateMap<CreateCommentViewModel, Comment>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore()); 
        }
    }
}
