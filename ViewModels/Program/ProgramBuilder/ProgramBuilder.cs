using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.ViewModels.User;

namespace CinemaVillage.ViewModels.Program.ProgramBuilder
{
    public class ProgramBuilder
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public ProgramBuilder(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService) 
        {
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public ProgramViewModel Build(string date)
        {
            var moviesIds = _moviesAppService.GetAllMoviesIds();

            var runningDatesForMovie = _movieXrefTheatreAppService.GetRunningDatesByIdsAndDate(moviesIds, date);
            foreach (var kvp in runningDatesForMovie)
            {
                kvp.Value.Sort();
            }

            var movieProgramPageAppModel = _moviesAppService.GetMoviesByIds(runningDatesForMovie);

            return new ProgramViewModel()
            {
                MovieProgramPageAppModel = movieProgramPageAppModel
            };
        }
    }
}
