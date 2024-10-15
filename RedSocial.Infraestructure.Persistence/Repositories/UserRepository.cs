

using Microsoft.EntityFrameworkCore;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using RedSocial.Infraestructure.Persistence.Context;

namespace RedSocial.Infraestructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByPhoneAsync(string phone)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Phone == phone);
        }


    }
}
