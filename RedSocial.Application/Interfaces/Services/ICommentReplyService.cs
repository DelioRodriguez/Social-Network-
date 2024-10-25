

using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Services
{
    public interface ICommentReplyService
    {
        Task<IEnumerable<CommentReply>> GetRepliesByCommentIdAsync(int commentId);
        Task<CommentReply> GetByIdAsync(int replyId);
        Task<CommentReply> CreateAsync(CommentReply commentReply);
        Task<bool> DeleteAsync(int replyId);
    }
}
