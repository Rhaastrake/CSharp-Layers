using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Data;
using MovieManager.DAL.Entities;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.DAL.Repositories
{
    public class MovieActorRepository : IMovieActorRepository
    {
        private readonly MovieDbContext _context;
        private readonly DbSet<MovieActor> _dbSet;

        public MovieActorRepository(MovieDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<MovieActor>();
        }

        public async Task<MovieActor?> GetByIdsAsync(int movieId, int actorId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.FindAsync(new object[] { movieId, actorId }, cancellationToken);
        }

        public async Task<IReadOnlyList<MovieActor>> GetByMovieIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(ma => ma.MovieId == movieId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int movieId, int actorId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AnyAsync(ma => ma.MovieId == movieId && ma.ActorId == actorId, cancellationToken);
        }

        public async Task AddAsync(MovieActor movieActor, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(movieActor, cancellationToken);
        }

        public void Remove(MovieActor movieActor)
        {
            _dbSet.Remove(movieActor);
        }
    }
}
