using CinemaVillage.AppModel.Movies;

namespace CinemaVillage.ViewModels.SeatSelection
{
    public class SeatSelectionViewModel
    {
        public MovieAppModel MovieAppModel { get; set; }
        public List<Seats> SeatsAvailability { get; set; }
        public string Date {  get; set; }
        public string Hour { get; set; }
        public int MovieId { get; set; }
        public int TheatreId { get; set; }
    }
}
