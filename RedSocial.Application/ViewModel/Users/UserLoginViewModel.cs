

using System.ComponentModel.DataAnnotations;

namespace RedSocial.Application.ViewModel.Users
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
