using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;

namespace CinemaVillage.Services.MovieXrefTheatreAppService
{
    public class MovieXrefTheatreAppService : IMovieXrefTheatreAppService
    {
        private readonly CinemaDbContext _context;

        public MovieXrefTheatreAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public void AddMovieXrefTheatre(MovieXrefTheatre movieXrefTheatreModel)
        {
            try
            {
                _context.MovieXrefTheatres.Add(movieXrefTheatreModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            _context.SaveChanges();
        }

        public List<string> GetAvailabilty(int theatreID)
        {
            return _context.MovieXrefTheatres.Where(mxt => mxt.IdTheatre == theatreID)
                                             .Select(mxt => mxt.Availability).ToList();
        }
    }
}
