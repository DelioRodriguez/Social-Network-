using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Repositories
{
    public interface ICommentReplyRepository
    {
        Task<IEnumerable<CommentReply>> GetRepliesByCommentIdAsync(int commentId);
        Task<CommentReply> GetByIdAsync(int replyId);
        Task<CommentReply> CreateAsync(CommentReply commentReply);
        Task<bool> DeleteAsync(int replyId);
    }
}
