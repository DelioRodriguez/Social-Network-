using Microsoft.EntityFrameworkCore;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Domain.Entities;
using RedSocial.Infraestructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Persistence.Repositories
{
    public class CommentReplyRepository : ICommentReplyRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentReplyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CommentReply>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await _context.CommentReplies
                .Where(cr => cr.CommentId == commentId)
                .Include(cr => cr.User)
                .ToListAsync();
        }

        public async Task<CommentReply> GetByIdAsync(int replyId)
        {
            return await _context.CommentReplies
                .Include(cr => cr.User)
                .Include(cr => cr.Comment)
                .FirstOrDefaultAsync(cr => cr.ReplyId == replyId);
        }

        public async Task<CommentReply> CreateAsync(CommentReply commentReply)
        {
            await _context.CommentReplies.AddAsync(commentReply);
            await _context.SaveChangesAsync();
            return commentReply;
        }

        public async Task<bool> DeleteAsync(int replyId)
        {
            var commentReply = await GetByIdAsync(replyId);
            if (commentReply == null)
            {
                return false;
            }

            _context.CommentReplies.Remove(commentReply);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
