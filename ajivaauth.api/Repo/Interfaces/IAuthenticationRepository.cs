using System.Threading.Tasks;
using ajivaauth.api.Models;
using AjivaAuth.Core.Models;

namespace ajivaauth.api.Repo.interfaces
{
    public interface IAuthenticationRepository
    {
        void Add<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<User> GetUser(string username);
        Task<bool> SaveAllChanges();
    }
}