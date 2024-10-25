
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Domain.Entities
{
    public class CommentReply
    {
        [Key]
        public int ReplyId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public Comment? Comment { get; set; }
        public User? User { get; set; }
    }
}
