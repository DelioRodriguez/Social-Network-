using RedSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.Interfaces.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
        Task<Comment> GetCommentByIdAsync(int id);
    }
}
