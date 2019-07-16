using Microsoft.EntityFrameworkCore;
using reusable_modules_sharing_server.Models;

namespace reusable_modules_sharing_server.Data
{
    public class UserModel : DbContext
    {
        public UserModel(DbContextOptions<UserModel> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
        }

    }
}
