
using Microsoft.EntityFrameworkCore;
using RedSocial.Domain.Entities;

namespace RedSocial.Infraestructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #region "Create DbSets"
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FriendShip> Friendships { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }
        public DbSet<CommentReply> CommentReplies{ get; set; }
        #endregion

        #region "Configurate Foreign Keys"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Post y User
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany(u => u.Posts)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de Comment y User
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de Comment y Post (Eliminar Post elimina Comments)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

           

            // Configuración de FriendShip y User
            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.User)
                .WithMany(u => u.Friendships)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendShip>()
                .HasOne(f => f.Friend)
                .WithMany()
                .HasForeignKey(f => f.FriendId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configuración de PasswordResetToken y User
            modelBuilder.Entity<PasswordResetToken>()
                .HasOne(t => t.User)
                .WithMany()
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de CommentReply y Comment (Eliminar Comment elimina CommentReplies)
            modelBuilder.Entity<CommentReply>()
                .HasOne(cr => cr.Comment)
                .WithMany(c => c.Replies)
                .HasForeignKey(cr => cr.CommentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de CommentReply y User
            modelBuilder.Entity<CommentReply>()
                .HasOne(cr => cr.User)
                .WithMany(u => u.CommentReplies)
                .HasForeignKey(cr => cr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        #endregion
    }
}
