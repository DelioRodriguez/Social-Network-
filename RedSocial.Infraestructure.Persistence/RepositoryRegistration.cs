using Microsoft.Extensions.DependencyInjection;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.Services;
using RedSocial.Infraestructure.Persistence.Repositories;


namespace RedSocial.Infraestructure.Persistence
{
    public static class RepositoryRegistration
    {
        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<ICommentReplyRepository, CommentReplyRepository>();

        }
    }
}
