using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);

        Task<Comment> GetByIdAsync(int id);
    }
}
