using System.Threading.Tasks;
using ajivaauth.api.Dtos;
using ajivaauth.api.Models;
using ajivaauth.api.Repo.interfaces;
using ajivaauth.api.Services.Interfaces;

namespace ajivaauth.api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _repo;

        public AuthenticationService(IAuthenticationRepository repo)
        {
            _repo = repo;    
        }

        public async Task<User> Login(UserDTO user)
        {
            var userFromDb = await _repo.GetUser(user.Username);
            
            if (userFromDb != null)
                if (!VerifyPasswordHash(user.Password, userFromDb.PasswordHash, userFromDb.PasswordSalt))
                    userFromDb = null;
            return userFromDb;
        }

        public async Task<User> Register(UserDTO user)
        {
            if (await UserExists(user.Username))
                return null;

            CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            var userToReturn = new User()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _repo.Add<User>(userToReturn);
            await _repo.SaveAllChanges();
            return userToReturn;
        }

        #region Private Methods
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) 
                        return false;
                }
            }

            return true;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private async Task<bool> UserExists(string username)
        {
            if (await _repo.GetUser(username) != null)
                return true;
            return false;
        }
        #endregion
    }
}