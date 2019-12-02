using ajivaauth.api.Models;
using AjivaAuth.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ajivaauth.api.Data
{
    public class DataContext :DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<User> Users { get; set; }
    }
}