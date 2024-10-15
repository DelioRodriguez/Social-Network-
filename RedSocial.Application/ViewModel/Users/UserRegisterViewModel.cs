using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RedSocial.Application.ViewModel.Users
{
    public class UserRegisterViewModel
    {
      
        [Required(ErrorMessage ="Nombre de la persona es obligatorio"), MaxLength(100),Display(Name ="Nombre")]
        public string FirstName { get; set; }

        [Required(ErrorMessage =" El apellido es obligatorio"), MaxLength(100),Display(Name ="Apellido")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="El nombre de usuario es obligatorio"), MaxLength(100),Display(Name ="Nombre de usuario")]
        public string Username { get; set; }

        [Required(ErrorMessage ="El correo electronico es obligatorio"), EmailAddress,Display(Name ="Correo Electronico")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Numero de telefono obligatorio"), Phone,Display(Name ="Numero de telefono")]
        [RegularExpression(@"^(809|829|849)\d{8}$|^([0-9]{10})$", ErrorMessage = "El número de teléfono no es válido. Debe estar en formato de República Dominicana.")]
      
        public string Phone { get; set; }

        [Required(ErrorMessage ="Imagen de perfil obligatoria"),Display(Name ="Foto de perfil")]
        public IFormFile ProfilePicture { get; set; }

        [Required(ErrorMessage ="La contraseña es obligatoria"),Display(Name ="Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="Confirmar la contraseña es obligatorio"), Display(Name = "Confirmar Contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Contraseñas no coinciden")]
        public string ConfirmPassword { get; set; }
    }
}
