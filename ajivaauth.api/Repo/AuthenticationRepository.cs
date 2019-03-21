using System.Threading.Tasks;
using ajivaauth.api.Data;
using ajivaauth.api.Models;
using ajivaauth.api.Repo.interfaces;
using Microsoft.EntityFrameworkCore;

namespace ajivaauth.api.Repo
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly DataContext _context;
        
        public AuthenticationRepository(DataContext context)        
        {
            _context = context;    
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }                

        public async Task<bool> SaveAllChanges()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}