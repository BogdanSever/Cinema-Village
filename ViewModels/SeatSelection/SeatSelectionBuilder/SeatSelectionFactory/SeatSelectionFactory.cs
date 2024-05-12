using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder.SeatSelectionFactory.Interface;

namespace CinemaVillage.ViewModels.SeatSelection.SeatSelectionBuilder.SeatSelectionFactory
{
    public class SeatSelectionFactory : ISeatSelectionFactory
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public SeatSelectionFactory(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public SeatSelectionBuilder CreateBuilder()
        {
            return new(_moviesAppService, _movieXrefTheatreAppService);
        }
    }
}
