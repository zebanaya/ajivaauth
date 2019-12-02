using System.Threading.Tasks;
using ajivaauth.api.Models;

namespace ajivaauth.api.Repo.Interfaces
{
    public interface IEmailService
    {
         Task<bool> SendContactUsEmail(ContactUsEmailDto contactUsEmailDto, SMPTConfig config);
         Task<bool> SendRegistrationEmail(ContactUsEmailDto contactUsEmailDto, SMPTConfig config);
    }
}