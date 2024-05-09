using CinemaVillage.AppModel.Theatres;

namespace CinemaVillage.Services.TheatreAppService.Interface
{
    public interface ITheatreAppService
    {
        List<TheatreAppModel> GetAllTheatres();
    }
}
