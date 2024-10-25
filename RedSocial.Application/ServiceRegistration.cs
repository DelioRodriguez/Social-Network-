using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.Services;
using System.Reflection;

namespace RedSocial.Application
{
    public static class ServiceRegistration
    {

        public static void RegisterService(IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddScoped<IPostService, PostService>();
            service.AddScoped<ICommentService, CommentService>();
            service.AddScoped<IFriendService, FriendService>();
            service.AddScoped<ICommentReplyService, CommentReplyService>();
            service.AddScoped<IUserService, UserService>();
        }
    }
}
