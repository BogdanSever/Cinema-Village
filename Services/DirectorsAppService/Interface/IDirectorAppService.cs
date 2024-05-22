using CinemaVillage.AppModel.Directors;
using CinemaVillage.Models;

namespace CinemaVillage.Services.DirectorsAppService.Interface
{
    public interface IDirectorAppService
    {
        List<DirectorsAppModel> GetAllDirectors();
        int GetDirectorId(string name);
        string GetDirectorName(int id);
        void AddDirector(Director directorModel);
        void DeleteDirector(Director directorModel);
        void UpdateDirector(Director directorModel);
    }
}
