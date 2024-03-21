using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Booking
{
    public int IdBooking { get; set; }

    public int IdUser { get; set; }

    public int IdMovieXrefTheatre { get; set; }

    public string SeatsBooked { get; set; } = null!;

    public DateTime BookingTime { get; set; }

    public virtual MovieXrefTheatre IdMovieXrefTheatreNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
