using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModel.Users;
using System.Security.Claims;

namespace RedSocialWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        public AccountController(IUserService user, IMapper mapper, IPostService postService)
        {
            _userService = user;
            _mapper = mapper;
            _postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var user = await _userService.LoginAsync(model);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Credenciales inválidas");
                    return View(model);
                }
                if (user.Id == 0) 
                {
                    ModelState.AddModelError(string.Empty, "Error al obtener el identificador del usuario.");
                    return View(model);
                }

                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username)
        };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
        public async Task<IActionResult> Profile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return NotFound(); 
            }

            int userId = int.Parse(userIdClaim.Value); 

            var user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var posts = await _postService.GetPostsByUserIdAsync(userId);
            var model = new UserProfileViewModel
            {
                ProfilePicture = user.ProfilePicture,
                FullName = $"{user.FirstName} {user.LastName}",
                Posts = posts
            };

            return View(model);
        }



        [AllowAnonymous]
        public IActionResult Register() => View(new UserRegisterViewModel());

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userService.RegisterAsync(model);
                return RedirectToAction("Login");
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _userService.ResetPasswordAsync(model);
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> Verify(int userId)
        {
            if (userId <= 0)
            {
                return NotFound();
            }


            await _userService.ActivateUserAsync(userId);
            ViewBag.Message = "Tu cuenta ha sido verificada exitosamente. Puedes iniciar sesión ahora.";
            return View("Verify");


        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userService.GetByIdAsync(userId); 

            var model = _mapper.Map<UserProfileEditViewModel>(user);

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(UserProfileEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                await _userService.UpdateProfileAsync(model, userId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

    }
}
