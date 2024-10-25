
using RedSocial.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Domain.Entities
{
    public class Comment : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
        public ICollection<CommentReply> Replies { get; set; }
    }
}
