using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Theatre
{
    public int IdTheatre { get; set; }

    public int Capacity { get; set; }

    public int NoOfRows { get; set; }

    public virtual ICollection<MovieXrefTheatre> MovieXrefTheatres { get; set; } = new List<MovieXrefTheatre>();
}
