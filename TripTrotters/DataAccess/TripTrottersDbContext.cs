using Microsoft.EntityFrameworkCore;
using TripTrotters.Models;

namespace TripTrotters.DataAccess
{
    public class TripTrottersDbContext : DbContext
    {
        public TripTrottersDbContext(DbContextOptions<TripTrottersDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

        }
    }
}
