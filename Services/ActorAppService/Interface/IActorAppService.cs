namespace CinemaVillage.Services.ActorAppService.Interface
{
    public interface IActorAppService
    {
        public List<string> GetCastByActorsIds(List<int> actorsIds);
    }
}
