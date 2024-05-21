using CinemaVillage.AppModel.Bookings;
using CinemaVillage.AppModel.Movies;
using CinemaVillage.AppModel.Reviews;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.HelperService;
using CinemaVillage.Services.HelperService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Drawing;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaVillage.Services.MoviesAppService
{
    public class MoviesAppService : IMoviesAppService
    {
        private readonly CinemaDbContext _context;
        private readonly IFormatDateTimeService _formatDateTimeService;

        public MoviesAppService(CinemaDbContext context, IFormatDateTimeService formatDateTimeService)
        {
            _context = context;
            _formatDateTimeService = formatDateTimeService;
        }

        public List<MovieAppModel> GetAllMovies()
        {
            var movieModel = _context.Movies.ToList() ?? throw new Exception();

            return movieModel.Select(m => new MovieAppModel
            {
                IdMovie = m.IdMovie,
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

        public List<MovieAppModel> GetAllMoviesInNext30Days()
        {
            List<MovieAppModel> movieAppModels = new List<MovieAppModel>();

            string currentDate = _formatDateTimeService.GetFormattedDate(DateTime.Now.ToString("d"));

            var moviesIds = GetAllMoviesIds();
            var movieXrefTheatresModel = _context.MovieXrefTheatres;

            foreach (var movieId in moviesIds)
            {
                var availability = movieXrefTheatresModel.Where(mxt => mxt.IdMovie == movieId).Select(mxt => mxt.Availability).FirstOrDefault();
                var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);

                bool inNext30Days = false;
                foreach (var entry in model)
                {
                    string jsonDate = _formatDateTimeService.GetFormattedDate(entry.Date);
                    
                    if (DateTime.Compare(DateTime.ParseExact(jsonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(currentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) >= 0)
                    {
                        inNext30Days = true;
                    }
                }

                if (inNext30Days)
                {
                    movieAppModels.Add(GetMovieById(movieId));
                }
            }

            return movieAppModels;
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
                IdMovie = model.IdMovie,
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

        public void DeleteMovieByMovieId(int movieId)
        {
            _context.Movies.Where(m => m.IdMovie == movieId).ExecuteDelete();
        }

        public void UpdateMovie(Movie movie)
        {
            if (CheckForExistanceMovie(movie.Title))
            {
                try
                {
                    _context.Movies
                        .Where(m => m.IdMovie == movie.IdMovie)
                        .ExecuteUpdate(up => up
                            .SetProperty(m => m.Title, movie.Title)
                            .SetProperty(m => m.Genre, movie.Genre)
                            .SetProperty(m => m.Duration, movie.Duration)
                            .SetProperty(m => m.ReleaseDate, movie.ReleaseDate)
                            .SetProperty(m => m.Discription, movie.Discription)
                        );
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
            else
            {
                throw new InvalidOperationException("No user found in DB!");
            }
        }
    }
}
