namespace CinemaVillage.AppModel.Bookings
{
    public class BookingAppModel
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdMovieXrefTheatre { get; set; }
        public string SeatsBooked { get; set; }
        public DateTime BookingTimeOfMovie { get; set; }
    }
}
