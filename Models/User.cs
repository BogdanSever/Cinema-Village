using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string FamilyName { get; set; } = null!;

    public string GivenName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
