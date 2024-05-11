using CinemaVillage.AppModel.Bookings;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
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

        public MovieAppModel GetMovieById(int id)
        {
            var movieModel = _context.Movies;

            return ToAppModel(movieModel.Where(m => m.IdMovie == id).FirstOrDefault());
        }

        private MovieAppModel ToAppModel(Movie model)
        {
            return new MovieAppModel
            {
                Title = model.Title,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Description = model.Discription,
                Image = TransformImage(model.Image)
            };
        }

        public (List<MovieUserPageAppModel>, List<MovieUserPageAppModel>) GetMovies(List<BookingAppModel> bookingAppModel)
        {
            List<MovieUserPageAppModel> futureMovies = new();
            List<MovieUserPageAppModel> pastMovies = new();

            var movieXrefTheatresModel = _context.MovieXrefTheatres;
            var movieModel = _context.Movies;

            foreach (var booking in bookingAppModel)
            {
                var movieFromXref = movieXrefTheatresModel.Where(m => m.IdScreenXrefMovie == booking.IdMovieXrefTheatre).FirstOrDefault();
                var movie = ToAppModel(movieModel.Where(m => m.IdMovie == movieFromXref.IdMovie).FirstOrDefault(), booking.BookingTimeOfMovie);

                if(movieFromXref.RunningDatetime < DateTime.Now)
                {
                    pastMovies.Add(movie);
                }
                else
                {
                    futureMovies.Add(movie);
                }
            }

            return (futureMovies, pastMovies);
        }

        private MovieUserPageAppModel ToAppModel(Movie model, DateTime bookingTimeMovie)
        {
            return new MovieUserPageAppModel
            {
                Title = model.Title,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Description = model.Discription,
                Image = TransformImage(model.Image),
                BookingTimeMovie = bookingTimeMovie
            };
        }

        public int AddMovie(Movie movieModel)
        {
            if (!CheckForExistanceMovie(movieModel.Title))
            {
                try
                {
                    _context.Movies.Add(movieModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                _context.SaveChanges();

                return movieModel.IdMovie;
            }
            else
            {
                throw new InvalidOperationException("Movie already exists!");
            }
        }

        private bool CheckForExistanceMovie(string title)
        {
            foreach (var movie in _context.Movies)
            {
                if (movie.Title.Equals(title))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
