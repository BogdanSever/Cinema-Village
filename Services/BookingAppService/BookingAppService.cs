using CinemaVillage.AppModel.Bookings;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.BookingAppService.Interface;
using CinemaVillage.Services.HelperService.Interface;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaVillage.Services.BookingAppService
{
    public class BookingAppService : IBookingAppService
    {
        private readonly CinemaDbContext _context;
        private readonly IFormatDateTimeService _formatDateTimeService;

        public BookingAppService(CinemaDbContext context, IFormatDateTimeService formatDateTimeService)
        {
            _context = context;
            _formatDateTimeService = formatDateTimeService;
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

        //TODO: CHECK ANOTHER DATE FORMAT PC
        public void AddBooking(int idMovieXrefTheatre, int userId, string date, string hour, List<int> seatsBooked)
        {
            var formattedDate = _formatDateTimeService.GetFormattedDate(date);
            var formattedHour = _formatDateTimeService.GetFormattedHour(hour);
            (var day, var month, var year) = _formatDateTimeService.GetSplittedDate(formattedDate);
            (var hours, var minutes, var seconds) = _formatDateTimeService.GetSplittedHour(formattedHour);

            if(formattedHour != null && formattedDate != null) 
            {
                var modelBooking = new Booking
                {
                    IdMovieXrefTheatre = idMovieXrefTheatre,
                    IdUser = userId,
                    SeatsBooked = string.Join(", ", seatsBooked),
                    BookingTime = new DateTime(year, month, day, hours, minutes, seconds),
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
            else
            {
                throw new InvalidOperationException("Date/hour are null!");
            }            
        }
    }
}
