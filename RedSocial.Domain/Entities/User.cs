
using RedSocial.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }
        [Required, MaxLength(100)]
        public string LastName { get; set; }
        [Required, MaxLength(100)]
        public string Username { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, Phone]
        public string Phone { get; set; }
        public string? ProfilePicture { get; set; }
        public bool IsActive { get; set; }


        public ICollection<Post> Posts { get; set; } = new List<Post>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<CommentReply> CommentReplies { get; set; }
        public ICollection<FriendShip> Friendships { get; set; } = new List<FriendShip>();


    }
}
