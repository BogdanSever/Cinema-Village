using System;
using System.Collections.Generic;

namespace CinemaVillage.Models;

public partial class Movie
{
    public int IdMovie { get; set; }

    public int IdDirector { get; set; }

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string Duration { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public string Discription { get; set; } = null!;

    public byte[]? Image { get; set; }

    public virtual ICollection<ActorsXrefMovie> ActorsXrefMovies { get; set; } = new List<ActorsXrefMovie>();

    public virtual Director IdDirectorNavigation { get; set; } = null!;

    public virtual ICollection<MovieXrefTheatre> MovieXrefTheatres { get; set; } = new List<MovieXrefTheatre>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
