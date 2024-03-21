using CinemaVillage.AppModel.Movies;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.MoviesAppService.Interface;
using System.Drawing;

namespace CinemaVillage.Services.MoviesAppService
{
    public class MoviesAppService : IMoviesAppService
    {
        private readonly CinemaDbContext _context;

        public MoviesAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public List<MovieAppModel> GetImagesMovies()
        {
            var movieModel = _context.Movies.ToList() ?? throw new Exception();

            return movieModel.Select(m => new MovieAppModel
            {
                Title = m.Title,
                Genre = m.Genre,
                Duration = m.Duration,
                ReleaseDate = m.ReleaseDate,
                Description = m.Discription,
                Image = TransformImage(m.Image)
            }).ToList();
        }

        private string TransformImage(byte[] image)
        {
            return Convert.ToBase64String(image);
        }
    }
}
