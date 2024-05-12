using CinemaVillage.AppModel.Bookings;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.BookingAppService.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaVillage.Services.BookingAppService
{
    public class BookingAppService : IBookingAppService
    {
        private readonly CinemaDbContext _context;

        public BookingAppService(CinemaDbContext context)
        {
            _context = context;
        }

        public List<BookingAppModel> GetAllBookingsByUserID(int userID)
        {
            var bookings = _context.Bookings;

            var currentUserBookings = ToAppModel(bookings.Where(b => b.IdUser == userID).ToList());

            return currentUserBookings;
        }

        private List<BookingAppModel> ToAppModel(List<Booking> model)
        {
            List<BookingAppModel> bookingsAppModel = new List<BookingAppModel>();

            if (model != null)
            {
                foreach (var booking in model)
                {
                    bookingsAppModel.Add(new BookingAppModel
                    {
                        Id = booking.IdBooking,
                        IdUser = booking.IdUser,
                        IdMovieXrefTheatre = booking.IdMovieXrefTheatre,
                        SeatsBooked = booking.SeatsBooked,
                        BookingTimeOfMovie = booking.BookingTime
                    });
                }
                
                return bookingsAppModel;
            }

            return null;
        }

        public void AddBooking(int idMovieXrefTheatre, int userId, string date, string hour, List<int> seatsBooked)
        {
            var modelBooking = new Booking
            {
                IdMovieXrefTheatre = idMovieXrefTheatre,
                IdUser = userId,
                SeatsBooked = string.Join(", ", seatsBooked),
                BookingTime = new DateTime(DateOnly.Parse(date), TimeOnly.Parse(hour)),
            };

            try
            {
                _context.Bookings.Add(modelBooking);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            _context.SaveChanges();
        }
    }
}
