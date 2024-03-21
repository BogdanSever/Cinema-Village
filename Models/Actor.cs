using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Actor
{
    public int IdActor { get; set; }

    public string FamilyName { get; set; } = null!;

    public string GivenName { get; set; } = null!;

    public virtual ICollection<ActorsXrefMovie> ActorsXrefMovies { get; set; } = new List<ActorsXrefMovie>();
}
