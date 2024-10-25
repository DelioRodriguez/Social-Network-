using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Domain.Entities;


namespace RedSocial.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }
        public async Task<Comment> GetCommentByIdAsync(int id)
        {
            return await _commentRepository.GetByIdAsync(id);
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            return await _commentRepository.CreateCommentAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            return await _commentRepository.DeleteCommentAsync(id);
        }
    }
}
