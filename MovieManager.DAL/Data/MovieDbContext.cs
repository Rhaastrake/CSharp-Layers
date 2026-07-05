using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Entities;

namespace MovieManager.DAL.Data;

public class MovieDbContext : DbContext {
    public DbSet<Movie> Movies;
    public DbSet<Genre> Genres;
    public DbSet<Director> Directors;
    public DbSet<Actor> Actors;
    public DbSet<MovieActor> MovieActors;
    public DbSet<Review> Reviews;

    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options) {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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


        modelBuilder.Entity<Movie>()
            .Property(m => m.Title)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Genre>()
            .Property(m => m.Name)
            .IsRequired()
            .HasMaxLength(30);

        modelBuilder.Entity<Actor>()
            .Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Actor>()
            .Property(m => m.LastName)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Director>()
            .Property(d => d.FirstName)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Director>()
            .Property(d => d.LastName)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Review>()
            .Property(m => m.ReviewerName)
            .IsRequired()
            .HasMaxLength(70);

        modelBuilder.Entity<Review>()
            .Property(m => m.Score)
            .IsRequired();

        modelBuilder.Entity<Review>()
            .ToTable(t => t.HasCheckConstraint("CK_Review_Score", "Score >= 1 AND Score <= 10"));

    }
}