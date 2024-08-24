using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace BlogAPI.Models
{
    public class Context :IdentityDbContext
    {
        public Context(DbContextOptions<Context> options) : base(options){ }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Default")
        //          .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Comment>()
                   .HasOne(c => c.User)
                   .WithMany(x => x.Comments)
                   .HasForeignKey(t => t.UserId);

            //builder.Entity<Like>()
            //       .HasOne(l => l.Post)
            //       .HasForeignKey(t => t.PostId)
            //       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Like>()
                .HasOne(l=>l.Comment)
                .WithMany(x=>x.Likes)
                .HasForeignKey(t => t.CommentId)
                .OnDelete(DeleteBehavior.Restrict);




        }
    }
}
