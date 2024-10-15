
using RedSocial.Domain.Common;

namespace RedSocial.Domain.Entities
{
    public class FriendShip : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int FriendId { get; set; }
        public User Friend { get; set; }
    }
}
