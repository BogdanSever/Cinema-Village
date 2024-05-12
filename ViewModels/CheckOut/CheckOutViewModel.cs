using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.ViewModels.CheckOut
{
    public class CheckOutViewModel
    {
        public MovieAppModel MovieAppModel { get; set; }
        public int NoOfSeatsAvailable { get; set; }
    }
}
