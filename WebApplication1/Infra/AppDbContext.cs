using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Infra
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User => Set<User>();
        public DbSet<Login> Login => Set<Login>();
        public DbSet<ValidateEmail> ValidateEmail => Set<ValidateEmail>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Login)
                .WithOne(l => l.User)
                .HasForeignKey<Login>(l => l.Id);

            base.OnModelCreating(modelBuilder);
        }

    }
}
