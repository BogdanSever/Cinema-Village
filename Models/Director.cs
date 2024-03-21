using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Director
{
    public int IdDirector { get; set; }

    public string FamilyName { get; set; } = null!;

    public string GivenName { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
