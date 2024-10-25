using RedSocial.Application.ViewModel.Posts;


namespace RedSocial.Application.ViewModel.Users
{
    public class UserProfileViewModel
    {
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        public List<PostViewModel> Posts { get; set; }
    }
}
