using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Review
{
    public int IdReview { get; set; }

    public int IdUser { get; set; }

    public int IdMovie { get; set; }

    public decimal NoOfStars { get; set; }

    public string? Description { get; set; }

    public virtual Movie IdMovieNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
