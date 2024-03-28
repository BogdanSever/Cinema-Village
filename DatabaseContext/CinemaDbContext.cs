using System;
using System.Collections.Generic;
using CinemaVillage.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaVillage.DatabaseContext;

public partial class CinemaDbContext : DbContext
{
    public CinemaDbContext()
    {
    }

    public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<ActorsXrefMovie> ActorsXrefMovies { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieXrefTheatre> MovieXrefTheatres { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Theatre> Theatres { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            throw new Exception("Db not configured!");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdActor);

            entity.Property(e => e.IdActor).HasColumnName("id_actor");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(50)
                .HasColumnName("family_name");
            entity.Property(e => e.GivenName)
                .HasMaxLength(50)
                .HasColumnName("given_name");
        });

        modelBuilder.Entity<ActorsXrefMovie>(entity =>
        {
            entity.HasKey(e => e.IdActorXrefMovie);

            entity.Property(e => e.IdActorXrefMovie).HasColumnName("id_actor_xref_movie");
            entity.Property(e => e.IdActor).HasColumnName("id_actor");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");

            entity.HasOne(d => d.IdActorNavigation).WithMany(p => p.ActorsXrefMovies)
                .HasForeignKey(d => d.IdActor)
                .HasConstraintName("FK_ActorsXrefMovies_Actors");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.ActorsXrefMovies)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_ActorsXrefMovies_Movies");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.IdBooking);

            entity.Property(e => e.IdBooking).HasColumnName("id_booking");
            entity.Property(e => e.BookingTime)
                .HasColumnType("datetime")
                .HasColumnName("booking_time");
            entity.Property(e => e.IdMovieXrefTheatre).HasColumnName("id_movie_xref_theatre");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.SeatsBooked).HasColumnName("seats_booked");

            entity.HasOne(d => d.IdMovieXrefTheatreNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.IdMovieXrefTheatre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_MovieXrefTheatre");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Users");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.IdDirector);

            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(50)
                .HasColumnName("family_name");
            entity.Property(e => e.GivenName)
                .HasMaxLength(50)
                .HasColumnName("given_name");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie);

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.Discription).HasColumnName("discription");
            entity.Property(e => e.Duration)
                .HasMaxLength(50)
                .HasColumnName("duration");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .HasColumnName("genre");
            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.IdDirectorNavigation).WithMany(p => p.Movies)
                .HasForeignKey(d => d.IdDirector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movies_Directors");
        });

        modelBuilder.Entity<MovieXrefTheatre>(entity =>
        {
            entity.HasKey(e => e.IdScreenXrefMovie);

            entity.ToTable("MovieXrefTheatre");

            entity.Property(e => e.IdScreenXrefMovie).HasColumnName("id_screen_xref_movie");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdTheatre).HasColumnName("id_theatre");
            entity.Property(e => e.RunningDatetime)
                .HasColumnType("datetime")
                .HasColumnName("running_datetime");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieXrefTheatres)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieXrefTheatre_Movies");

            entity.HasOne(d => d.IdTheatreNavigation).WithMany(p => p.MovieXrefTheatres)
                .HasForeignKey(d => d.IdTheatre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MovieXrefTheatre_Theatres");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.IdReview);

            entity.Property(e => e.IdReview).HasColumnName("id_review");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.NoOfStars)
                .HasColumnType("numeric(1, 1)")
                .HasColumnName("no_of_stars");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdMovie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Movies");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<Theatre>(entity =>
        {
            entity.HasKey(e => e.IdTheatre);

            entity.Property(e => e.IdTheatre).HasColumnName("id_theatre");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.NoOfRows).HasColumnName("no_of_rows");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser);

            entity.Property(e => e.IdUser)
                .HasColumnName("id_user");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.FamilyName)
                .HasMaxLength(50)
                .HasColumnName("family_name");
            entity.Property(e => e.GivenName)
                .HasMaxLength(50)
                .HasColumnName("given_name");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
