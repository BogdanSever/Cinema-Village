using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.ViewModels.AllMovie.AllMovieBuilder.AllMovieFactory.Interface;

namespace CinemaVillage.ViewModels.AllMovie.AllMovieBuilder.AllMovieFactory
{
    public class AllMovieFactory : IAllMovieFactory
    {
        private readonly IMoviesAppService _movieAppService;

        public AllMovieFactory(IMoviesAppService movieAppService)
        {
            _movieAppService = movieAppService;
        }

        public AllMovieBuilder CreateBuilder()
        {
            return new(_movieAppService);
        }
    }
}
