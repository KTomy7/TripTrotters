using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TripTrotters.Models;

namespace TripTrotters.DataAccess
{
    public class TripTrottersDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public TripTrottersDbContext(DbContextOptions<TripTrottersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Apartment)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.ApartmentId);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Offers)
                .WithOne(o => o.Apartment)
                .HasForeignKey(o => o.ApartmentId);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Agent)
                .WithMany(u => u.Offers)
                .HasForeignKey(o => o.AgentId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Review> Rewies { get; set; }
    }
}
