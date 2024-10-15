using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByUsernameAsync(string username);
        Task<User> FindByEmailAsync(string email);
        Task<User> FindByPhoneAsync(string phone);
    }
}
