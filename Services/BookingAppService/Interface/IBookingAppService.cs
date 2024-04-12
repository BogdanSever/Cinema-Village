using CinemaVillage.AppModel.Bookings;

namespace CinemaVillage.Services.BookingAppService.Interface
{
    public interface IBookingAppService
    {
        List<BookingAppModel> GetAllBookingsByUserID(int userID);
    }
}
