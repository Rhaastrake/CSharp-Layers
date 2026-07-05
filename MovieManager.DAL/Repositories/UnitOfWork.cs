using Microsoft.EntityFrameworkCore;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.DAL.Repositories;

public class UnitOfWork : IUnitOfWork {
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(DbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);

        if (_repositories.TryGetValue(type, out var existing))
        {
            return (IGenericRepository<T>)existing;
        }

        var repository = new GenericRepository<T>(_context);
        _repositories[type] = repository;

        return repository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}