using CinemaVillage.AppModel.Directors;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.DirectorsAppService.Interface;


namespace CinemaVillage.Services.DirectorsAppService
{
    public class DirectorAppService : IDirectorAppService
    {
        public readonly CinemaDbContext _context;

        public DirectorAppService(CinemaDbContext context)
        {
            _context = context;
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
    }
}
