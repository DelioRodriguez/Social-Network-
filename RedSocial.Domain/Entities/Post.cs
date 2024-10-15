
using RedSocial.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Domain.Entities
{
    public class Post : BaseEntity
    {
        [Required]
        public string Content { get; set; }

        public string? ImagePath { get; set; }

        public string? YoutubeUrl { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    }
}
