using System.Threading.Tasks;
using ajivaauth.api.Models;

namespace ajivaauth.api.Data
{
    public interface IAuthenticationRepository
    {
         Task<User> Register(User user, string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}