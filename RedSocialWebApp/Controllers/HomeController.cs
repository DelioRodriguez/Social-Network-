using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModel.Comments;
using RedSocial.Application.ViewModel.Home;
using RedSocial.Application.ViewModel.Posts;
using RedSocial.Domain.Entities;
using System.Security.Claims;

namespace RedSocialWebApp.Controllers
{
    [Authorize] // Asegúrate de que el controlador esté protegido
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public HomeController(IPostService postService, ICommentService commentService)
        {
            _postService = postService;
            _commentService = commentService;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener todos los posts
            var posts = await _postService.GetAllPostsAsync();
            var postViewModels = posts.Select(post => new PostViewModel
            {
                Id = post.Id,
                Content = post.Content,
                ImagePath = post.ImagePath,
                YoutubeUrl = post.YoutubeUrl,
                UserName = $"{post.User.FirstName} {post.User.LastName}",
                CreatedAt = post.CreatedAt,
                Comments = post.Comments.Select(comment => new CommentViewModel
                {
                    Id = comment.Id,
                    Content = comment.Content,
                    UserName = $"{comment.User.FirstName} {comment.User.LastName}",
                    CreatedAt = comment.CreatedAt,
                    PostId = comment.PostId
                }).ToList()
            }).ToList();

            var viewModel = new HomeViewModel
            {
                Posts = postViewModels
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if (!ModelState.IsValid) return View("Index");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtener el UserId desde el claim

            var post = new Post
            {
                Content = model.Content,
                ImagePath = model.ImagePath,
                YoutubeUrl = model.YoutubeUrl,
                UserId = int.Parse(userId) // Asegúrate de que el UserId sea del tipo correcto
            };

            await _postService.CreatePostAsync(post);
            return RedirectToAction("Index");
        }

        // Otros métodos...
    }
}
