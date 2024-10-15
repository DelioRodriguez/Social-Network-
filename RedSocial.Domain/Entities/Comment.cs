
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

        public int? ParentCommentId { get; set; }
        public Comment ParentComment { get; set; }

        public ICollection<Comment> Replies { get; set; } = new List<Comment>();
    }
}
