using CinemaVillage.AppModel.Theatres;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.TheatreAppService.Interface;
using Microsoft.EntityFrameworkCore;

namespace CinemaVillage.Services.TheatreAppService
{
    public class TheatreAppService : ITheatreAppService
    {
        private readonly CinemaDbContext _context;

        public TheatreAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public List<TheatreAppModel> GetAllTheatres()
        {
            var theatresModel = _context.Theatres.ToList();
            List<TheatreAppModel> theatreAppModel = new();


            foreach (var theatre in theatresModel)
            {
                theatreAppModel.Add(new TheatreAppModel
                {
                    Id = theatre.IdTheatre,
                    NoOfRows = theatre.NoOfRows,
                    Capacity = theatre.Capacity
                });
            }

            return theatreAppModel;
        }

        public void AddTheatre(Theatre theatreModel)
        {
            try
            {
                _context.Theatres.Add(theatreModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            _context.SaveChanges();
        }

        public void DeleteTheatre(int theatreId)
        {
            _context.Theatres.Where(t => t.IdTheatre == theatreId).ExecuteDelete();
        }
    }
}
