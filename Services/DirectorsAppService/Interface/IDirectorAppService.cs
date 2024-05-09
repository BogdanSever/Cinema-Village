using CinemaVillage.AppModel.Directors;

namespace CinemaVillage.Services.DirectorsAppService.Interface
{
    public interface IDirectorAppService
    {
        List<DirectorsAppModel> GetAllDirectors();
    }
}
