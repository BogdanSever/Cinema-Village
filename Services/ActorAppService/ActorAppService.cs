using CinemaVillage.DatabaseContext;
using CinemaVillage.Services.ActorAppService.Interface;

namespace CinemaVillage.Services.ActorAppService
{
    public class ActorAppService : IActorAppService
    {
        private readonly CinemaDbContext _context;

        public ActorAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public List<string> GetCastByActorsIds(List<int> actorsIds)
        {
            List<string> cast = new List<string>();
            
            foreach (int actorId in actorsIds)
            {
                cast.Add(_context.Actors.Where(a => a.IdActor == actorId).Select(a => a.GivenName + " " + a.FamilyName).FirstOrDefault());
            }

            return cast;
        }
    }
}
