using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModel.Comments;
using RedSocial.Application.ViewModel.Friends;
using RedSocial.Application.ViewModel.Posts;
using RedSocial.Application.ViewModel.Users;
using RedSocial.Domain.Entities;
using System.Security.Claims;

namespace RedSocialWebApp.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendService _friendService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public FriendsController(IFriendService friendService, ICommentService commentService, IMapper mapper)
        {
            _friendService = friendService;
            _commentService = commentService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                return BadRequest("Error al obtener el ID del usuario.");
            }

            var friends = await _friendService.GetFriendsAsync(userId);
            var posts = await _friendService.GetFriendPostsAsync(userId);

            var viewModel = new FriendsViewModel
            {
                Friends = _mapper.Map<List<UserViewModel>>(friends),
                Posts = _mapper.Map<List<PostViewModel>>(posts)
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("", "El nombre de usuario es obligatorio.");
                return RedirectToAction("Index");
            }

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdString, out var userId))
            {
                ModelState.AddModelError("", "Error al obtener el ID de usuario.");
                return RedirectToAction("Index");
            }

            var result = await _friendService.AddFriendAsync(userId, username);
            if (!result)
            {
                ModelState.AddModelError("", "El usuario no existe.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFriend(int friendId)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (int.TryParse(userIdString, out var userId))
            {
                try
                {
                    await _friendService.RemoveFriendAsync(userId, friendId);
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Ocurrió un error al eliminar al amigo. Intente nuevamente.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Error al obtener el ID de usuario.");
            }

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
