using CinemaVillage.Services.ImdbApiService.Interface;
using CinemaVillage.Services.ImdbApiService.Models;
using Newtonsoft.Json;

namespace CinemaVillage.Services.ImdbApiService
{
    public class ImdbApiService : IImdbApiService
    {
        public async Task<ImdbResponse> GetRatingAsync(string movieTitle)
        {
            var id = await GetIMDBId(movieTitle);

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/title/v2/get-ratings?tconst={id}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "265ee6f0fcmshe07eef9391bca63p1ad23bjsn8334afb573bf" },
                    { "X-RapidAPI-Host", "imdb8.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                dynamic imdbResponse = JsonConvert.DeserializeObject(body);
                int voteCount = imdbResponse.data.title.ratingsSummary.voteCount;
                float aggregateRating = imdbResponse.data.title.ratingsSummary.aggregateRating;
                
                return new ImdbResponse
                {
                    Rating = aggregateRating,
                    VoteCount = voteCount,
                };
            }
        }

        private async Task<string> GetIMDBId(string movieTitle)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://imdb8.p.rapidapi.com/v2/search?searchTerm={movieTitle.Replace(" ", "")}&type=MOVIE&first=1"),
                Headers =
                {
                    { "X-RapidAPI-Key", "162fb2acbfmsh236ac4e746333f5p1783fajsn2bc4c3bcf33b" },
                    { "X-RapidAPI-Host", "imdb8.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                dynamic imdbResponse = JsonConvert.DeserializeObject(body);
                string id = imdbResponse.data.mainSearch.edges[0].node.entity.id;
                return id;
            }  
        }
    }
}
