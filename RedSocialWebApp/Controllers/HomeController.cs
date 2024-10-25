using AutoMapper;
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
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ICommentReplyService _commentReplyService;
        private readonly IMapper _mapper;

        public HomeController(IPostService postService, ICommentService commentService, IMapper mapper, ICommentReplyService commentReplyService)
        {
            _postService = postService;
            _commentService = commentService;
            _mapper = mapper;
            _commentReplyService = commentReplyService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();

            var postViewModels = _mapper.Map<List<PostViewModel>>(posts);

            var viewModel = new HomeViewModel
            {
                Posts = postViewModels,
                CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if ((model.ImagePath != null && model.ImagePath.Length > 0) &&
                string.IsNullOrWhiteSpace(model.Content) ||
                (!string.IsNullOrEmpty(model.YoutubeUrl) && string.IsNullOrWhiteSpace(model.Content)))
            {
                ModelState.AddModelError("Content", "Por favor, proporciona un mensaje adicional sobre la imagen o el video.");
                return RedirectToAction("Index");
            }

            var post = _mapper.Map<Post>(model);
            post.UserId = userId;

            if (model.ImagePath != null && model.ImagePath.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/images_post", model.ImagePath.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImagePath.CopyToAsync(stream);
                }
                post.ImagePath = $"/images/images_post/{model.ImagePath.FileName}";
            }

            if (!string.IsNullOrEmpty(model.YoutubeUrl))
            {
                string videoId = null;
                var uri = new Uri(model.YoutubeUrl);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                if (query["v"] != null)
                {
                    videoId = query["v"];
                }
                else if (uri.Host.Contains("youtu.be"))
                {
                    videoId = uri.Segments.Last();
                }

                if (!string.IsNullOrEmpty(videoId))
                {
                    post.YoutubeUrl = $"https://www.youtube.com/embed/{videoId}"; 
                }
                else
                {
                    ModelState.AddModelError("YoutubeUrl", "El enlace de YouTube no es válido.");
                    return RedirectToAction("Index");
                }
            }

            await _postService.CreatePostAsync(post);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var comment = _mapper.Map<Comment>(model);
            comment.UserId = userId;

            await _commentService.CreateCommentAsync(comment);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var posts = await _postService.GetAllPostsAsync();
            var post = posts.FirstOrDefault(p => p.Id == id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (post == null || post.UserId != userId)
            {
                return Forbid(); 
            }

            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> CreateReply(CreateCommentReplyViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var reply = _mapper.Map<CommentReply>(model);
            reply.UserId = userId;

            await _commentReplyService.CreateAsync(reply);
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var reply = await _commentReplyService.GetByIdAsync(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (reply == null || reply.UserId != userId)
            {
                return Forbid();
            }

            await _commentReplyService.DeleteAsync(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (comment == null || comment.UserId != userId)
            {
                return Forbid(); 
            }

            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
