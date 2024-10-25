using RedSocial.Application.ViewModel.Comments;
using RedSocial.Application.ViewModel.Posts;

namespace RedSocial.Application.ViewModel.Home
{
    public class HomeViewModel
    {
        public List<PostViewModel> Posts { get; set; } = new List<PostViewModel>();

        public int CurrentUserId { get; set; }
   
    }
}
