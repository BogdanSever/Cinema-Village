using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;

namespace CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder
{
    public class SeatSelectionBuilder
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public SeatSelectionBuilder(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public SeatSelectionViewModel Build(string date, string hour, int movieId, int theatreId)
        {

            var movieAppModel = _moviesAppService.GetMovieById(movieId);
            var seats = _movieXrefTheatreAppService.GetSeatsAvailability(date, hour, movieId, theatreId);

            return new SeatSelectionViewModel()
            {
                MovieAppModel = movieAppModel,
                SeatsAvailability = seats,
                Date = date,
                Hour = hour,
                MovieId = movieId,
                TheatreId = theatreId
            };
        }
    }
}
