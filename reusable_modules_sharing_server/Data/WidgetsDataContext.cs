using Microsoft.EntityFrameworkCore;
using reusable_modules_sharing_server.Models;
using reusable_modules_sharing_server.ViewModels;
using WidgetServer.Models;

namespace WidgetServer.Data
{
    public class WidgetsDataContext : DbContext
    {
        public WidgetsDataContext(DbContextOptions<WidgetsDataContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Widget> Widgets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Widget>().ToTable("Widget");
        }

    }
}
