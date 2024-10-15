using AutoMapper;
using RedSocial.Application.Dtos.Email;
using RedSocial.Application.Interfaces.Repositories;
using RedSocial.Application.Interfaces.Services;
using RedSocial.Application.ViewModel.Users;
using RedSocial.Domain.Entities;

namespace RedSocial.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IEmailService emailService, IMapper mapper)
        {
            _repository = repository;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<UserLoginViewModel> LoginAsync(UserLoginViewModel model)
        {
            var user = await _repository.FindByUsernameAsync(model.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                throw new InvalidDataException("Nombre de usuario o contraseña son incorrectos");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException("Usuario no se encuentra activo, por favor verifica tu email");
            }
            return _mapper.Map<UserLoginViewModel>(user);
            
        }


        public async Task<UserRegisterViewModel> RegisterAsync(UserRegisterViewModel model)
        {
            var existingUserByUsername = await _repository.FindByUsernameAsync(model.Username);
            var existingUserByEmail = await _repository.FindByEmailAsync(model.Email);
            var existingUserByPhone = await _repository.FindByPhoneAsync(model.Phone);

            if (existingUserByUsername != null)
            {
                throw new InvalidOperationException("El nombre de usuario ya está en uso.");
            }

            if (existingUserByEmail != null)
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
            }

            if (existingUserByPhone != null)
            {
                throw new InvalidOperationException("El número de teléfono ya está registrado.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

            var user = _mapper.Map<User>(model);
            user.IsActive = false; 
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password); 

            if (model.ProfilePicture != null && model.ProfilePicture.Length > 0)
            {
                var fileExtension = Path.GetExtension(model.ProfilePicture.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException("Formato de archivo no permitido. Solo se permiten archivos con extensiones .jpg, .jpeg o .png.");
                }

                var fileName = $"{Guid.NewGuid()}_{model.ProfilePicture.FileName}";
                var filePath = Path.Combine("wwwroot/images/profile_pictures", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(stream);
                }

                user.ProfilePicture = $"/images/profile_pictures/{fileName}"; 
            }

            await _repository.AddAsync(user);
            await _repository.SaveChangesAsync();

            await SendVerificationEmailAsync(user); 

            return _mapper.Map<UserRegisterViewModel>(user); 
        }





        public async Task ResetPasswordAsync(ForgotPasswordViewModel model)
        {
            var user = await _repository.FindByUsernameAsync(model.Username);

            if (user == null)
            {
                throw new InvalidOperationException("User no existe");
            }

            var newPassword = GenerateRandomPassword();
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            await _repository.UpdateAsync(user);

            var emailRequest = new EmailRequest
            {
                To = user.Email,
                Subject = "Password Reset",
                Body = $"Su contraseña ha cambiado, ahora es: {newPassword}"
            };

            await _emailService.SendAsync(emailRequest);
        }

        public async Task SendVerificationEmailAsync(User user)
        {
            var verificationLink = $"http://localhost:5000/Account/Verify?userId={user.Id}";

            var emailRequest = new EmailRequest
            {
                To = user.Email,
                Subject = "Email Verification",
                Body = $"Por favor verifica tu email clickeando <a href=\"{verificationLink}\">aquí</a>"
            };

            await _emailService.SendAsync(emailRequest);
        }


        public async Task ActivateUserAsync(int userId)
        {
            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("Usuario no encontrado");
            }

            user.IsActive = true;
            await _repository.UpdateAsync(user);
        }

        private string GenerateRandomPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

       
    }
}
