using CinemaVillage.AppModel.Directors;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.ReviewAppService.Interface;
using Microsoft.EntityFrameworkCore;
using System.Drawing;


namespace CinemaVillage.Services.DirectorsAppService
{
    public class DirectorAppService : IDirectorAppService
    {
        public readonly CinemaDbContext _context;
        private readonly IMoviesAppService _moviesAppService;
        private readonly IReviewAppService _reviewAppService;
        private readonly IActorXrefMovieAppService _actorXrefMovieAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public DirectorAppService(CinemaDbContext context, IMoviesAppService moviesAppService, IReviewAppService reviewAppService, 
                                  IActorXrefMovieAppService actorXrefMovieAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _context = context;
            _moviesAppService = moviesAppService;
            _reviewAppService = reviewAppService;
            _actorXrefMovieAppService = actorXrefMovieAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public List<DirectorsAppModel> GetAllDirectors()
        {
            var listDirectorsAppModel = new List<DirectorsAppModel>();
            var directorsModel = _context.Directors;

            foreach (var director in directorsModel)
            {
                listDirectorsAppModel.Add(new DirectorsAppModel
                {
                    Id = director.IdDirector,
                    FirstName = director.GivenName,
                    LastName = director.FamilyName,
                });
            }

            return listDirectorsAppModel;
        }

        public int GetDirectorId(string name)
        {
            return _context.Directors.Where(d => d.GivenName == name).Select(d => d.IdDirector).FirstOrDefault();
        }

        public string GetDirectorName(int idDirector)
        {
            return _context.Directors.Where(d => d.IdDirector == idDirector).Select(d => d.GivenName + " " + d.FamilyName).FirstOrDefault();
        }

        public void AddDirector(Director directorModel)
        {
            if (!CheckForDirectorExistance(directorModel))
            {
                try
                {
                    _context.Directors.Add(directorModel);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }

                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User already exists!");
            }
        }

        private bool CheckForDirectorExistance(Director directorModel)
        {
            return _context.Directors.Any(d => d.IdDirector == directorModel.IdDirector);
        }

        public void DeleteDirector(Director directorModel)
        {
            if (CheckForDirectorExistance(directorModel))
            {
                var moviesIds = _context.Movies.Where(m => m.IdDirector == directorModel.IdDirector).Select(m => m.IdMovie).ToList();

                foreach(var movieId in moviesIds)
                {
                    _reviewAppService.DeleteReviewsByMovieId(movieId);
                    _actorXrefMovieAppService.DeleteActorsXrefMovieByMovieId(movieId);
                    _movieXrefTheatreAppService.DeleteMovieXrefTheatreByMovieId(movieId);
                    _moviesAppService.DeleteMovieByMovieId(movieId);
                }

                int noOfRowsDeleted = _context.Directors.Where(u => u.IdDirector == directorModel.IdDirector).ExecuteDelete();

                if (noOfRowsDeleted == 0)
                {
                    throw new InvalidOperationException("There are no rows deleted, even though there was found a director");
                }
            }
            else
            {
                throw new InvalidOperationException("No director found in DB!");
            }
        }

        public void UpdateDirector(Director directorModel)
        {
            if (CheckForDirectorExistance(directorModel))
            {
                try
                {
                    _context.Directors
                        .Where(u => u.IdDirector == directorModel.IdDirector)
                        .ExecuteUpdate(up => up
                            .SetProperty(d => d.FamilyName, directorModel.FamilyName)
                            .SetProperty(d => d.GivenName, directorModel.GivenName)
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
