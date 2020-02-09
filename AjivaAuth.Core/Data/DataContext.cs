using AjivaAuth.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AjivaAuth.Core.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) {}
        public DbSet<User> Users { get; set; }
    }
}