using RedSocial.Application.ViewModel.Posts;
using RedSocial.Application.ViewModel.Users;


namespace RedSocial.Application.ViewModel.Friends
{
    public class FriendsViewModel
    {
        public List<UserViewModel>?Friends { get; set; }
        public List<PostViewModel>? Posts { get; set; }
        public string CurrentUserId { get; set; }

    }

}
