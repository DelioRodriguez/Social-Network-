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

        Task ActivateUserAsync(int userId);
    }

}
