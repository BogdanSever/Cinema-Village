using CinemaVillage.Models;

namespace CinemaVillage.Services.MovieXrefTheatreAppService.Interface
{
    public interface IMovieXrefTheatreAppService
    {
        void AddMovieXrefTheatre(MovieXrefTheatre movieXrefTheatreModel);
        List<string> GetAvailabilty(int theatreID);
    }
}
