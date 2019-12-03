using System.Threading.Tasks;
using AjivaAuth.Core.Models;

namespace AjivaAuth.Repo.interfaces
{
    public interface IAuthenticationRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<User> GetUser(string username);
        Task<bool> SaveAllChanges();
    }
}