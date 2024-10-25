using System.ComponentModel.DataAnnotations;


namespace RedSocial.Application.ViewModel.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }  

        [Display(Name = "Nombre")]
        public string FirstName { get; set; }

        [Display(Name = "Apellido")]
        public string LastName { get; set; }

        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Display(Name = "Foto de perfil")]
        public string ProfilePicture { get; set; } 
    }
}
