using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;

namespace CinemaVillage.Services.ActorXrefMovieAppService
{
    public class ActorXrefMovieAppService : IActorXrefMovieAppService
    {
        private readonly CinemaDbContext _context;

        public ActorXrefMovieAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public List<int> GetAllActorsByMovieId(int movieId)
        {
            return _context.ActorsXrefMovies.Where(axm => axm.IdMovie == movieId).Select(axm => axm.IdActor).ToList();
        }
    }
}
