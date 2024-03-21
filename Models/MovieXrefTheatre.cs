using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class MovieXrefTheatre
{
    public int IdScreenXrefMovie { get; set; }

    public int IdTheatre { get; set; }

    public int IdMovie { get; set; }

    public DateTime RunningDatetime { get; set; }

    public string Availability { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Movie IdMovieNavigation { get; set; } = null!;

    public virtual Theatre IdTheatreNavigation { get; set; } = null!;
}
