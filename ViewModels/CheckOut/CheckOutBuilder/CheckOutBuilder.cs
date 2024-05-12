using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;

namespace CinemaVillage.ViewModels.CheckOut.CheckOutBuilder
{
    public class CheckOutBuilder
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public CheckOutBuilder(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService) 
        { 
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public CheckOutViewModel Build(string date, string hour, string movieID, string theatreID)
        {
            var theatre_id = Int32.Parse(theatreID);
            var movie_id = Int32.Parse(movieID);

            var movieModel = _moviesAppService.GetMovieById(movie_id);
            var noOfSeatsAvailable = _movieXrefTheatreAppService.GetNoOfSeatsAvailable(date, hour, movie_id, theatre_id);

            return new CheckOutViewModel()
            {
                MovieAppModel = movieModel,
                NoOfSeatsAvailable = noOfSeatsAvailable,
                TheatreId = theatre_id,
                MovieId = movie_id,
                Date = date,
                Hour = hour,
            };
        }
    }
}
