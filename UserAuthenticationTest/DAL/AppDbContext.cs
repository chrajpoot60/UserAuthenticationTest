using Microsoft.EntityFrameworkCore;
using UserAuthenticationTest.Models;

namespace UserAuthenticationTest.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasKey(x => x.user_id);

        }
    }
}
