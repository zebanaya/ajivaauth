using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ajivaauth.api.Data;
using ajivaauth.api.Dtos;
using ajivaauth.api.Models;
using ajivaauth.api.Services.Interfaces;
using AjivaAuth.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ajivaauth.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IAuthenticationService _authService;
        public readonly IConfiguration _config;

        public AuthenticationController(IAuthenticationService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            var userFromDB = await _authService.Register(userDTO);

            if (userFromDB == null)
                return BadRequest("Username already exists");
            return Ok(userFromDB);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserDTO userDTO)
        {
            var userToLogin = await _authService.Login(userDTO);

            if (userToLogin == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userToLogin.Id.ToString()),
                new Claim(ClaimTypes.Name, userToLogin.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}