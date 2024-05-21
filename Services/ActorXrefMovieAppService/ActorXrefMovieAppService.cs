using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using Microsoft.EntityFrameworkCore;

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

        public void DeleteActorsXrefMovieByMovieId(int movieId)
        {
            var actorsXrefMovies = _context.ActorsXrefMovies.Where(axm => axm.IdMovie == movieId).ToList();

            foreach (var actorXrefMovie in actorsXrefMovies)
            {
                _context.ActorsXrefMovies.Where(axm => axm.IdMovie == actorXrefMovie.IdMovie).ExecuteDelete();
            }
        }
    }
}
