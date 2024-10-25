using Microsoft.EntityFrameworkCore;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using RedSocial.Infraestructure.Persistence.Context;


namespace RedSocial.Infraestructure.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .Include(c => c.Replies)
                    .ThenInclude(r => r.User) 
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            comment.CreatedAt = DateTime.UtcNow;
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null) return false;

            _context.Comments.Remove(comment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
