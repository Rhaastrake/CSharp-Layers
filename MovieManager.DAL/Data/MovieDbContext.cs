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

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        
    }
    
}