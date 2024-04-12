using CinemaVillage.AppModel.Bookings;
using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.Services.MoviesAppService.Interface
{
    public interface IMoviesAppService
    {
        List<MovieAppModel> GetImagesMovies();
        MovieAppModel GetMovieById(int id);
        (List<MovieUserPageAppModel> futureMovies, List<MovieUserPageAppModel> pastMovies) GetMovies(List<BookingAppModel> bookingAppModel);
    }
}
