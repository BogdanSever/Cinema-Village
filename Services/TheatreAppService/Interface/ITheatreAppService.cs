using CinemaVillage.AppModel.Theatres;
using CinemaVillage.Models;

namespace CinemaVillage.Services.TheatreAppService.Interface
{
    public interface ITheatreAppService
    {
        List<TheatreAppModel> GetAllTheatres();
        void AddTheatre(Theatre theatreModel);
        void DeleteTheatre(int theatreId);
    }
}
