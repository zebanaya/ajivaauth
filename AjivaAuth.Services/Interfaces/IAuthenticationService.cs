using System.Threading.Tasks;
using AjivaAuth.Core.DTOs;
using AjivaAuth.Core.Models;

namespace AjivaAuth.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Register(UserDTO user);
        Task<User> Login(UserDTO user);         
    }
}