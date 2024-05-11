using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.HelperService.Interface;
using Newtonsoft.Json;

namespace CinemaVillage.Services.HelperService
{
    public class JsonCreatorService : IJsonCreatorService
    {
        public string CreateJson(List<MovieAddJsonAppModel> moviesJson)
        {
            return JsonConvert.SerializeObject(moviesJson);
        }
    }
}
