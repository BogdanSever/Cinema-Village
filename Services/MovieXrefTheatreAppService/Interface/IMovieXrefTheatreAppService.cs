using CinemaVillage.Models;
using System.Drawing;

namespace CinemaVillage.Services.MovieXrefTheatreAppService.Interface
{
    public interface IMovieXrefTheatreAppService
    {
        void AddMovieXrefTheatre(MovieXrefTheatre movieXrefTheatreModel);
        List<string> GetAvailabilty(int theatreID);
        Dictionary<int, List<string>> GetRunningDatesByIdsAndDate(List<int> moviesIds, string date);
        int GetNoOfSeatsAvailable(string date, string hour, int movieID, int theatreID);
    }
}
