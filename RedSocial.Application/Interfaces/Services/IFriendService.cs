using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IFriendService
    {
        Task<List<User>> GetFriendsAsync(int userId);
        Task<List<Post>> GetFriendPostsAsync(int userId);
        Task<bool> AddFriendAsync(int userId, string friendUsername);
        Task RemoveFriendAsync(int userId, int friendId);
    }
}
