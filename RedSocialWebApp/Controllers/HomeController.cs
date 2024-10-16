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
        private readonly IMapper _mapper;

        public HomeController(IPostService postService, ICommentService commentService, IMapper mapper)
        {
            _postService = postService;
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPostsAsync();

            // Mapeo de las publicaciones a PostViewModel usando AutoMapper
            var postViewModels = _mapper.Map<List<PostViewModel>>(posts);

            var viewModel = new HomeViewModel
            {
                Posts = postViewModels,
                CurrentUserId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)) // Obtener el ID del usuario autenticado
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostViewModel model)
        {
            // Si el modelo no es válido, redirigir a "Index"
            if (!ModelState.IsValid) return RedirectToAction("Index");

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Verificar si el contenido está vacío cuando se intenta publicar una imagen o un video
            if ((model.ImagePath != null && model.ImagePath.Length > 0) &&
                string.IsNullOrWhiteSpace(model.Content) ||
                (!string.IsNullOrEmpty(model.YoutubeUrl) && string.IsNullOrWhiteSpace(model.Content)))
            {
                ModelState.AddModelError("Content", "Por favor, proporciona un mensaje adicional sobre la imagen o el video.");
                return RedirectToAction("Index");
            }


            // Mapeo del modelo a la entidad Post
            var post = _mapper.Map<Post>(model);
            post.UserId = userId;

            // Manejar la subida de la imagen
            if (model.ImagePath != null && model.ImagePath.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/images_post", model.ImagePath.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImagePath.CopyToAsync(stream);
                }
                post.ImagePath = $"/images/images_post/{model.ImagePath.FileName}";
            }

            // Asignar el enlace de YouTube si existe
            if (!string.IsNullOrEmpty(model.YoutubeUrl))
            {
                // Validar el enlace de YouTube y extraer el ID
                string videoId = null;
                var uri = new Uri(model.YoutubeUrl);
                var query = System.Web.HttpUtility.ParseQueryString(uri.Query);

                // Extraer el ID del video de la URL
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
                    post.YoutubeUrl = $"https://www.youtube.com/embed/{videoId}"; // Guardar el enlace del iframe
                }
                else
                {
                    ModelState.AddModelError("YoutubeUrl", "El enlace de YouTube no es válido.");
                    return RedirectToAction("Index");
                }
            }

            // Crear la publicación
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
                return Forbid(); // Denegar acceso si el post no le pertenece
            }

            await _postService.DeletePostAsync(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (comment == null || comment.UserId != userId)
            {
                return Forbid(); // Denegar acceso si el comentario no le pertenece
            }

            await _commentService.DeleteCommentAsync(id);
            return RedirectToAction("Index");
        }
    }
}
