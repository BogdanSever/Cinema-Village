using CinemaVillage.AppModel.Bookings;

namespace CinemaVillage.Services.BookingAppService.Interface
{
    public interface IBookingAppService
    {
        List<BookingAppModel> GetAllBookingsByUserID(int userID);
        void AddBooking(int idMovieXrefTheatre, int userId, string date, string hour, List<int> seatsBooked);
    }
}
