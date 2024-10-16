using RedSocial.Application.ViewModel.Comments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Application.ViewModel.Posts
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string? ImagePath { get; set; }
        public string? YoutubeUrl { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public string UserProfileImage { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
