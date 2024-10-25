

using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Services
{
    public class CommentReplyService : ICommentReplyService
    {
        private readonly ICommentReplyRepository _commentReplyRepository;

        public CommentReplyService(ICommentReplyRepository commentReplyRepository)
        {
            _commentReplyRepository = commentReplyRepository;
        }

        public async Task<IEnumerable<CommentReply>> GetRepliesByCommentIdAsync(int commentId)
        {
            return await _commentReplyRepository.GetRepliesByCommentIdAsync(commentId);
        }

        public async Task<CommentReply> GetByIdAsync(int replyId)
        {
            return await _commentReplyRepository.GetByIdAsync(replyId);
        }

        public async Task<CommentReply> CreateAsync(CommentReply commentReply)
        {
            return await _commentReplyRepository.CreateAsync(commentReply);
        }

        public async Task<bool> DeleteAsync(int replyId)
        {
            return await _commentReplyRepository.DeleteAsync(replyId);
        }
    }
    }
