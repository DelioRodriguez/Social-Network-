
using RedSocial.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Domain.Entities
{
    public class PasswordResetToken : BaseEntity
    {
        [Required]
        public string Token { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.Now.AddHours(1);

        public int UserId { get; set; }
        public User User { get; set; }

    }
}
