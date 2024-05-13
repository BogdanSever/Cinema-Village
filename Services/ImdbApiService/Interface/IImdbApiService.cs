using CinemaVillage.Services.ImdbApiService.Models;

namespace CinemaVillage.Services.ImdbApiService.Interface
{
    public interface IImdbApiService
    {
        public Task<ImdbResponse> GetRatingAsync(string title);
    }
}
