using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class ActorsXrefMovie
{
    public int IdActorXrefMovie { get; set; }

    public int IdActor { get; set; }

    public int IdMovie { get; set; }

    public virtual Actor IdActorNavigation { get; set; }

    public virtual Movie IdMovieNavigation { get; set; }
}
