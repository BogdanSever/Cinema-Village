using CinemaVillage.AppModel.Theatres;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.TheatreAppService.Interface;

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
    }
}
