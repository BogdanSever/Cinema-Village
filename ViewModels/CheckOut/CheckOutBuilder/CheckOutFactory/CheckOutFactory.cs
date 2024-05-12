using CinemaVillage.Services.MoviesAppService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using CinemaVillage.ViewModels.CheckOut.CheckOutBuilder.CheckOutFactory.Interface;

namespace CinemaVillage.ViewModels.CheckOut.CheckOutBuilder.CheckOutFactory
{
    public class CheckOutFactory : ICheckOutFactory
    {
        private readonly IMoviesAppService _moviesAppService;
        private readonly IMovieXrefTheatreAppService _movieXrefTheatreAppService;

        public CheckOutFactory(IMoviesAppService moviesAppService, IMovieXrefTheatreAppService movieXrefTheatreAppService) 
        { 
            _moviesAppService = moviesAppService;
            _movieXrefTheatreAppService = movieXrefTheatreAppService;
        }


        public CheckOutBuilder CreateBuilder()
        {
            return new(_moviesAppService, _movieXrefTheatreAppService);
        }
    }
}
