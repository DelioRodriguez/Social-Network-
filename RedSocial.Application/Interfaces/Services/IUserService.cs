using RedSocial.Application.ViewModel.Users;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserRegisterViewModel> RegisterAsync(UserRegisterViewModel model);
        Task<UserLoginViewModel> LoginAsync(UserLoginViewModel model);
        Task SendVerificationEmailAsync(User user);
        Task ResetPasswordAsync(ForgotPasswordViewModel model);
        Task UpdateProfileAsync(UserProfileEditViewModel model, int userId);
        Task<User> GetByIdAsync(int userId);
        Task ActivateUserAsync(int userId);
    }

}
