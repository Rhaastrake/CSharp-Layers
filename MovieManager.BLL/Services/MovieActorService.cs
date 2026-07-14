using AutoMapper;
using MovieManager.BLL.Models;
using MovieManager.BLL.Services.Interfaces;
using MovieManager.DAL.Entities;
using MovieManager.DAL.Repositories.Interfaces;

namespace MovieManager.BLL.Services
{
    public class MovieActorService : IMovieActorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMovieActorRepository _movieActorRepository;
        private readonly IMapper _mapper;

        public MovieActorService(IUnitOfWork unitOfWork, IMovieActorRepository movieActorRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _movieActorRepository = movieActorRepository;
            _mapper = mapper;
        }

        public async Task<MovieActorModel?> GetByIdsAsync(int movieId, int actorId, CancellationToken cancellationToken = default)
        {
            var entity = await _movieActorRepository.GetByIdsAsync(movieId, actorId, cancellationToken);
            return entity == null ? null : _mapper.Map<MovieActorModel>(entity);
        }

        public async Task<IReadOnlyList<MovieActorModel>> GetByMovieIdAsync(int movieId, CancellationToken cancellationToken = default)
        {
            var entities = await _movieActorRepository.GetByMovieIdAsync(movieId, cancellationToken);
            return _mapper.Map<IReadOnlyList<MovieActorModel>>(entities);
        }

        public async Task<MovieActorModel> CreateAsync(MovieActorModel model, CancellationToken cancellationToken = default)
        {
            var entity = _mapper.Map<MovieActor>(model);
            await _movieActorRepository.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var savedEntity = await _movieActorRepository.GetByIdsAsync(entity.MovieId, entity.ActorId, cancellationToken);
            return _mapper.Map<MovieActorModel>(savedEntity ?? entity);
        }

        public async Task<bool> DeleteAsync(int movieId, int actorId, CancellationToken cancellationToken = default)
        {
            var entity = await _movieActorRepository.GetByIdsAsync(movieId, actorId, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            _movieActorRepository.Remove(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        
        public async Task<bool> UpdateAsync(MovieActorModel model, CancellationToken cancellationToken = default)
        {
            var entity = await _movieActorRepository.GetByIdsAsync(model.MovieId, model.ActorId, cancellationToken);
            if (entity == null)
            {
                return false;
            }

            entity.CharacterName = model.CharacterName;
            entity.IsLeadRole = model.IsLeadRole;
            entity.DisplayOrder = model.DisplayOrder;

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
