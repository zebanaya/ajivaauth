using System.Threading.Tasks;
using ajivaauth.api.Dtos;
using ajivaauth.api.Models;
using AjivaAuth.Core.DTOs;
using AjivaAuth.Core.Models;

namespace ajivaauth.api.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<User> Register(UserDTO user);
        Task<User> Login(UserDTO user);         
    }
}