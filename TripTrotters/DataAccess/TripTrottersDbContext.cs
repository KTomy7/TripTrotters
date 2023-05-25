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
                .HasForeignKey(p => p.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Offers)
                .WithOne(o => o.Apartment)
                .HasForeignKey(o => o.ApartmentId);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Reviews)
                .WithOne(r => r.Apartment)
                .HasForeignKey(r => r.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Apartment>()
                .HasOne(ow => ow.Owner)
                .WithMany(a => a.Apartments)
                .HasForeignKey(ow => ow.OwnerId);
            //.OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Offer>()
                .HasOne(o => o.Agent)
                .WithMany(u => u.Offers)
                .HasForeignKey(o => o.AgentId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Address)
                .WithOne(ad => ad.Apartment)
                .HasForeignKey<Apartment>(a => a.AddressId);

            modelBuilder.Entity<Apartment>()
                .HasMany(a => a.Images)
                .WithOne(i => i.Apartment)
                .HasForeignKey(i => i.ApartmentId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Post)
                .HasForeignKey(i => i.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserPostLike>()
                .HasKey(upl => new { upl.UserId, upl.PostId });

            modelBuilder.Entity<UserPostLike>()
                .HasOne(upl => upl.User)
                .WithMany(upl => upl.LikedPosts)
                .HasForeignKey(upl => upl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserPostLike>()
                .HasOne(upl => upl.Post)
                .WithMany(upl => upl.UsersLikes)
                .HasForeignKey(upl => upl.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserCommentLike>()
                .HasKey(ucl => new { ucl.UserId, ucl.CommentId });

            modelBuilder.Entity<UserCommentLike>()
                .HasOne(ucl => ucl.User)
                .WithMany(ucl => ucl.LikedComments)
                .HasForeignKey(ucl => ucl.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<UserCommentLike>()
                .HasOne(ucl => ucl.Comment)
                .WithMany(ucl => ucl.UsersLikes)
                .HasForeignKey(ucl => ucl.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<UserPostLike> UserPostLikes { get; set; }
        public DbSet<UserCommentLike> UserCommentLikes { get; set; }


        public override int SaveChanges()
        {
            DeleteImagesOfDeletedPosts();
            return base.SaveChanges();
        }

        private void DeleteImagesOfDeletedPosts()
        {
            var deletedPosts = ChangeTracker.Entries<Post>()
                .Where(e => e.State == EntityState.Deleted)
                .Select(e => e.Entity);

            foreach (var post in deletedPosts)
            {
                var imagesToDelete = Images.Where(i => i.PostId == post.Id);
                Images.RemoveRange(imagesToDelete);
            }
        }
    }
}

