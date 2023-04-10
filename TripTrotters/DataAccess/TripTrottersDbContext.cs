using Microsoft.EntityFrameworkCore;
using TripTrotters.Models;

namespace TripTrotters.DataAccess
{
    public class TripTrottersDbContext : DbContext
    {
        public TripTrottersDbContext()
        {
        }
        public TripTrottersDbContext(DbContextOptions<TripTrottersDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Initial Catalog=TripTrotters;Integrated Security=True;Encrypt=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Apartament)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.ApartamentId);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId);

            modelBuilder.Entity<Apartament>()
                .HasMany(a => a.Offers)
                .WithOne(o => o.Apartament)
                .HasForeignKey(o => o.ApartamentId);

            modelBuilder.Entity<Apartament>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Apartament)
                .HasForeignKey(r => r.ApartamentId);

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

        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Apartament> Apartaments { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Review> Rewies { get; set; }





    }

}
