using Microsoft.AspNetCore.Http;

namespace RedSocial.Application.ViewModel.Posts
{
    public class CreatePostViewModel
    {
        public string Content { get; set; }
        public IFormFile? ImagePath { get; set; }
        public string? YoutubeUrl { get; set; }
    }
}
