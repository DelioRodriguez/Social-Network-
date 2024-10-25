

using Microsoft.EntityFrameworkCore;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using RedSocial.Infraestructure.Persistence.Context;

namespace RedSocial.Infraestructure.Persistence.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendRepository( ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddFriendAsync(int userId, string friendUsername)
        {
            var friend = await _context.Users.FirstOrDefaultAsync(u => u.Username == friendUsername);
            if (friend == null || friend.Id == userId) return false;

            var friendShip = new FriendShip { UserId = userId, FriendId = friend.Id };
            _context.Friendships.Add(friendShip);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Post>> GetFriendPostsAsync(int userId)
        {
            var friendIDs = await _context.Friendships
                .Where(x => x.UserId == userId)
                .Select(fs => fs.FriendId)
                .ToListAsync();

            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.Comments) 
                    .ThenInclude(c => c.User) 
                .Where(p => friendIDs.Contains(p.UserId))
                .ToListAsync();
        }


        public async Task<List<User>> GetFriendsAsync(int userId)
        {
           return await _context.Friendships
                .Where( fs => fs.UserId == userId )
                .Select( fs => fs.Friend)
                .ToListAsync();
        }


        public async Task RemoveFriendAsync(int userId, int friendId)
        {
            var friendRelationship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.UserId == userId && f.FriendId == friendId);

            if (friendRelationship != null)
            {
                _context.Friendships.Remove(friendRelationship);
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new InvalidOperationException("No se encontró la relación de amistad.");
            }
        }
    }
}
