using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendRepository _friendRepository;

        public FriendService(IFriendRepository friend)
        {
            _friendRepository = friend;
        }
        public async Task<bool> AddFriendAsync(int userId, string friendUsername)
        {
            return await _friendRepository.AddFriendAsync(userId, friendUsername);
        }

        public async Task<List<Post>> GetFriendPostsAsync(int userId)
        {
            return await _friendRepository.GetFriendPostsAsync(userId);
        }

        public async Task<List<User>> GetFriendsAsync(int userId)
        {
            return await _friendRepository.GetFriendsAsync(userId);
        }

        public async Task RemoveFriendAsync(int userId, int friendId)
        {
            await _friendRepository.RemoveFriendAsync(userId, friendId);
        }
    }
}
