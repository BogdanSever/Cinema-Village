namespace CinemaVillage.Services.ActorXrefMovieAppService.Interface
{
    public interface IActorXrefMovieAppService
    {
        List<int> GetAllActorsByMovieId(int movieId);
        void DeleteActorsXrefMovieByMovieId(int movieId);
    }
}
