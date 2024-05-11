using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.Services.HelperService.Interface
{
    public interface IJsonCreatorService
    {
        public string CreateJson(List<MovieAddJsonAppModel> moviesJson);
    }
}
