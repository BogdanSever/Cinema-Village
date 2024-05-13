using CinemaVillage.AppModel.Bookings;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Reviews;
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

        public List<MovieAppModel> GetAllMovies()
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
                Image = TransformImage(model.Image),
                IdDirector = model.IdDirector
            };
        }

        public List<int> GetAllMoviesIds()
        {
            return _context.Movies.Select(m => m.IdMovie).ToList();
        }

        public (List<MovieUserPageAppModel>, List<MovieUserPageAppModel>, List<ReviewedMovieUserPageAppModel>) GetMovies(List<BookingAppModel> bookingAppModel)
        {
            List<MovieUserPageAppModel> futureMovies = new();
            List<MovieUserPageAppModel> pastMovies = new();
            List<ReviewedMovieUserPageAppModel> reviewedMovies = new();

            var movieXrefTheatresModel = _context.MovieXrefTheatres;
            var movieModel = _context.Movies;

            foreach (var booking in bookingAppModel)
            {
                var movieFromXref = movieXrefTheatresModel.Where(m => m.IdScreenXrefMovie == booking.IdMovieXrefTheatre).FirstOrDefault();
                var movie = ToAppModel(movieModel.Where(m => m.IdMovie == movieFromXref.IdMovie).FirstOrDefault(), booking.BookingTimeOfMovie, booking.SeatsBooked);

                if (_context.Reviews.Any(r => r.IdMovie == movie.Id && r.IdUser == booking.IdUser))
                {
                    var movieAlreadyReviewed = false;
                    foreach (var reviewedMovie in reviewedMovies)
                    {
                        if (reviewedMovie.IdMovie == movie.Id)
                        {
                            movieAlreadyReviewed = true;
                        }
                    }

                    if (!movieAlreadyReviewed)
                    {
                        var review = _context.Reviews.Where(r => r.IdMovie == movie.Id && r.IdUser == booking.IdUser).FirstOrDefault();
                        reviewedMovies.Add(new ReviewedMovieUserPageAppModel
                        {
                            IdMovie = movie.Id,
                            NoOfStars = review.NoOfStars,
                            Review = review.Description,
                            Image = movie.Image,
                            Title = movie.Title
                        });
                    }
                }
                else if (booking.BookingTimeOfMovie < DateTime.Now)
                {
                    var movieAlreadyPast = false;
                    foreach (var pastMovie in pastMovies)
                    {
                        if (pastMovie.Id == movie.Id)
                        {
                            movieAlreadyPast = true;
                        }
                    }

                    if (!movieAlreadyPast)
                    {
                        pastMovies.Add(movie);
                    }
                }
                
                if (booking.BookingTimeOfMovie >= DateTime.Now)
                {
                    futureMovies.Add(movie);
                }
            }

            return (futureMovies, pastMovies, reviewedMovies);
        }

        private MovieUserPageAppModel ToAppModel(Movie model, DateTime bookingTimeMovie, string seatsBooked)
        {
            return new MovieUserPageAppModel
            {
                Id = model.IdMovie,
                Title = model.Title,
                Genre = model.Genre,
                Duration = model.Duration,
                ReleaseDate = model.ReleaseDate,
                Description = model.Discription,
                Image = TransformImage(model.Image),
                BookingTimeMovie = bookingTimeMovie,
                SeatsBooked = seatsBooked,
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

        public List<MovieProgramPageAppModel> GetMoviesByIds(Dictionary<int, List<string>> runningHours)
        {
            var moviesProgramPageAppModel = new List<MovieProgramPageAppModel>();

            foreach (var kvp in runningHours)
            {
                var movieModel = _context.Movies.Where(m => m.IdMovie == kvp.Key).FirstOrDefault();
                var theatreId = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == kvp.Key).Select(mxt => mxt.IdTheatre).FirstOrDefault();

                moviesProgramPageAppModel.Add(new MovieProgramPageAppModel
                {
                    Title = movieModel.Title,
                    Genre = movieModel.Genre,
                    Id = movieModel.IdMovie,
                    Image = TransformImage(movieModel.Image),
                    RunningHours = kvp.Value,
                    IdTheatre = theatreId
                });
            }

            return moviesProgramPageAppModel;
        }
    }
}
