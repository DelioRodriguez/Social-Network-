

namespace RedSocial.Application.ViewModel.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string? UserProfileImage { get; set; }

        public List<CommentReplyViewModel>? Replies { get; set; } = new List<CommentReplyViewModel>();
    }
}
