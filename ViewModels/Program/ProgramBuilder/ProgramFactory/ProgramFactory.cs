using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory.Interface;
using CinemaVillage.ViewModels.User.UserBuilder;

namespace CinemaVillage.ViewModels.Program.ProgramBuilder.ProgramFactory
{
    public class ProgramFactory : IProgramFactory
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public ProgramFactory(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public ProgramBuilder CreateBuilder()
        {
            return new(_moviesAppService, _movieXrefTheatreAppService);
        }
    }
}
