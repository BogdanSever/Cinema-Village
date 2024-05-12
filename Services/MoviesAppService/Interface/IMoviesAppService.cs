using CinemaVillage.AppModel.Bookings;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Reviews;
using CinemaVillage.Models;

namespace CinemaVillage.Services.MoviesAppService.Interface
{
    public interface IMoviesAppService
    {
        List<MovieAppModel> GetAllMovies();
        MovieAppModel GetMovieById(int id);
        List<MovieProgramPageAppModel> GetMoviesByIds(Dictionary<int, List<string>> runningHours);
        List<int> GetAllMoviesIds();
        (List<MovieUserPageAppModel> futureMovies, List<MovieUserPageAppModel> pastMovies, List<ReviewedMovieUserPageAppModel> reviewedMovies) GetMovies(List<BookingAppModel> bookingAppModel);
        int AddMovie(Movie movie);
    }
}
