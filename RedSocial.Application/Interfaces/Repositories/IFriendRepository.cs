using RedSocial.Domain.Entities;


namespace RedSocial.Application.Interfaces.Repositories
{
    public interface IFriendRepository
    {
        Task<List<User>> GetFriendsAsync(int userId);
        Task<List<Post>> GetFriendPostsAsync(int userId);
        Task<bool> AddFriendAsync(int userId, string friendUsername);
        Task RemoveFriendAsync(int userId, int friendId);
    }
}
