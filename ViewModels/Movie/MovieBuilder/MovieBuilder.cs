﻿using CinemaVillage.AppModel.Movies;
using CinemaVillage.Services.ActorAppService.Interface;
using CinemaVillage.Services.ActorXrefMovieAppService.Interface;
using CinemaVillage.Services.DirectorsAppService.Interface;
using CinemaVillage.Services.ImdbApiService.Interface;
using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.Services.ReviewAppService.Interface;

namespace CinemaVillage.ViewModels.Movie.MovieBuilder
{
    public class MovieBuilder
    {
        //image, title, director, cast, description
        private readonly IMoviesAppService _movieAppService;
        private readonly IDirectorAppService _directorAppService;
        private readonly IActorAppService _actorAppService;
        private readonly IActorXrefMovieAppService _actorXrefMovieAppService;
        private readonly IImdbApiService _imdbApiService;
        private readonly IReviewAppService _reviewAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public MovieBuilder(IMoviesAppService movieAppService, IDirectorAppService directorAppService, 
                            IActorAppService actorAppService, IActorXrefMovieAppService actorXrefMovieAppService, 
                            IImdbApiService imdbApiService, IReviewAppService reviewAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService)
        {
            _movieAppService = movieAppService;
            _directorAppService = directorAppService;
            _actorAppService = actorAppService;
            _actorXrefMovieAppService = actorXrefMovieAppService;
            _imdbApiService = imdbApiService;
            _reviewAppService = reviewAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }

        public async Task<MovieViewModel> Build(int movieId)
        {
            var movieAppModel = _movieAppService.GetMovieById(movieId);
            var directorName = _directorAppService.GetDirectorName(movieAppModel.IdDirector);
            var actorsIds = _actorXrefMovieAppService.GetAllActorsByMovieId(movieId);
            var cast = _actorAppService.GetCastByActorsIds(actorsIds);

            var reviews = _reviewAppService.GetAllReviewsByMovieId(movieId);
            var ratings = await _imdbApiService.GetRatingAsync(movieAppModel.Title);

            var schedule = _movieXrefTheatreAppService.GetScheduleByMovieId(movieId);

            return new MovieViewModel()
            {
                Id = movieId,
                Title = movieAppModel.Title,
                Description = movieAppModel.Description,
                Image = movieAppModel.Image,
                DirectorName = directorName,
                Cast = cast,
                Ratings = ratings,
                Reviews = reviews,
                MovieSchedule = schedule
            };
        }
    }
}
