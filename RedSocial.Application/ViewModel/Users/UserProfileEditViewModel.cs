using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Application.ViewModel.Users
{
    public class UserProfileEditViewModel
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }
    }
}
