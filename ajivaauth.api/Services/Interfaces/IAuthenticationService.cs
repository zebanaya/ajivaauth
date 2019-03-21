using System.Threading.Tasks;
using ajivaauth.api.Dtos;
using ajivaauth.api.Models;

namespace ajivaauth.api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Register(UserDTO user);
        Task<User> Login(UserDTO user);         
    }
}