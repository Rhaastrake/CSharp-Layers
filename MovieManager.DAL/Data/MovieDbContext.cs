using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Entities;

namespace MovieManager.DAL.Data
{
    public class MovieDbContext : DbContext
    {
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Director> Directors => Set<Director>();
        public DbSet<Actor> Actors => Set<Actor>();
        public DbSet<MovieActor> MovieActors => Set<MovieActor>();
        public DbSet<Review> Reviews => Set<Review>();

        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieActor>()
               .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(ma => ma.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(m => m.Title)
                      .IsRequired()
                      .HasMaxLength(200);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(g => g.Name)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Director>(entity =>
            {
                entity.Property(d => d.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(d => d.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(a => a.FirstName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(a => a.LastName)
                      .IsRequired()
                      .HasMaxLength(100);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(r => r.ReviewerName)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.ToTable(t => t.HasCheckConstraint("CK_Review_Score", "[Score] >= 1 AND [Score] <= 10"));
            });
        }
    }
}
