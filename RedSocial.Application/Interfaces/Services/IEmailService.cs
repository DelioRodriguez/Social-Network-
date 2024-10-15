
using RedSocial.Application.Dtos.Email;
using RedSocial.Domain.Settings;

namespace RedSocial.Application.Interfaces.Services
{
    public interface IEmailService
    {
      
        Task SendAsync(EmailRequest request);
    }
}
