using System.ComponentModel.DataAnnotations;

namespace RedSocial.Application.ViewModel.Users
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string Username { get; set; }
    }

}
