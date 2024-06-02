using CinemaVillage.AppModel.Movies;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.HelperService.Interface;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CinemaVillage.Services.MovieXrefTheatreAppService
{
    public class MovieXrefTheatreAppService : IMovieXrefTheatreAppService
    {
        private readonly CinemaDbContext _context;
        private readonly IFormatDateTimeService _formatDateTimeService;

        public MovieXrefTheatreAppService(CinemaDbContext context, IFormatDateTimeService formatDateTimeService)
        {
            _context = context;
            _formatDateTimeService = formatDateTimeService;
        }

        public void AddMovieXrefTheatre(MovieXrefTheatre movieXrefTheatreModel)
        {
            try
            {
                _context.MovieXrefTheatres.Add(movieXrefTheatreModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            _context.SaveChanges();
        }

        public List<string> GetAvailabilty(int theatreID)
        {
            return _context.MovieXrefTheatres.Where(mxt => mxt.IdTheatre == theatreID)
                                             .Select(mxt => mxt.Availability).ToList();
        }

        //TODO: REFACTOR
        public Dictionary<int, List<string>> GetRunningDatesByIdsAndDate(List<int> moviesIds, string date)
        {
            string dateQueryParam = _formatDateTimeService.GetFormattedDate(date);

            Dictionary<int, List<string>> dictDatesAndHours = new();

            foreach (int id in moviesIds)
            {
                var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == id).Select(mxt => mxt.Availability).FirstOrDefault();
                if (availability != null)
                {
                    var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
                    foreach (var entry in model)
                    {
                        string jsonDate = _formatDateTimeService.GetFormattedDate(entry.Date);

                        if (DateTime.Compare(DateTime.ParseExact(dateQueryParam, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) == 0)
                        {
                            var hours = new List<string>();
                            foreach (var hourRunning in entry.HoursRunning)
                            {
                                string currentDate = _formatDateTimeService.GetFormattedDate(DateTime.Now.ToString("d"));

                                if (DateTime.Compare(DateTime.ParseExact(dateQueryParam, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(currentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) == 0)
                                {
                                    string currentHour = _formatDateTimeService.GetFormattedHour(DateTime.Now.ToString("HH:mm:ss"));
                                    string jsonHour = _formatDateTimeService.GetFormattedHour(hourRunning.Hour);

                                    if (DateTime.Compare(DateTime.ParseExact(currentHour, "HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonHour, "HH:mm:ss", CultureInfo.InvariantCulture)) <= 0)
                                    {
                                        bool available = false;
                                        foreach (var seat in hourRunning.Seats)
                                        {
                                            if (seat.Available == true)
                                            {
                                                available = true;
                                            }
                                        }

                                        if (available == true)
                                        {
                                            hours.Add(hourRunning.Hour);
                                        }
                                    }
                                }
                                else
                                {
                                    bool available = false;
                                    foreach (var seat in hourRunning.Seats)
                                    {
                                        if (seat.Available == true)
                                        {
                                            available = true;
                                        }
                                    }

                                    if (available == true)
                                    {
                                        hours.Add(hourRunning.Hour);
                                    }
                                }
                            }
                            if (hours.Any())
                            {
                                if (!dictDatesAndHours.ContainsKey(id))
                                {
                                    dictDatesAndHours.Add(id, hours);
                                }
                                else
                                {
                                    dictDatesAndHours[id].AddRange(hours);
                                }
                            }
                        }
                    }
                }
            }

            return dictDatesAndHours;
        }

        public int GetNoOfSeatsAvailable(string date, string hour, int movieID, int theatreID)
        {
            int noOfSeatsAvailable = 0;
            string dateQueryParam = _formatDateTimeService.GetFormattedDate(date);
            string hourQueryParam = _formatDateTimeService.GetFormattedHour(hour);

            var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieID && mxt.IdTheatre == theatreID).Select(mxt => mxt.Availability).FirstOrDefault();
            var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
            foreach (var entry in model)
            {
                string jsonDate = _formatDateTimeService.GetFormattedDate(entry.Date);

                if (DateTime.Compare(DateTime.ParseExact(dateQueryParam, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) == 0)
                {
                    foreach (var hourRunning in entry.HoursRunning)
                    {
                        string jsonHour = _formatDateTimeService.GetFormattedHour(hourRunning.Hour);

                        if (DateTime.Compare(DateTime.ParseExact(hourQueryParam, "HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonHour, "HH:mm:ss", CultureInfo.InvariantCulture)) == 0)
                        {
                            foreach (var seat in hourRunning.Seats)
                            {
                                if (seat.Available == true)
                                {
                                    noOfSeatsAvailable++;
                                }
                            }
                        }
                    }
                }
            }

            return noOfSeatsAvailable;
        }

        public List<Seats> GetSeatsAvailability(string date, string hour, int movieId, int theatreId)
        {
            string dateQueryParam = _formatDateTimeService.GetFormattedDate(date);
            string hourQueryParam = _formatDateTimeService.GetFormattedHour(hour);

            var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieId && mxt.IdTheatre == theatreId).Select(mxt => mxt.Availability).FirstOrDefault();
            var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
            foreach (var entry in model)
            {
                string jsonDate = _formatDateTimeService.GetFormattedDate(entry.Date);

                if (DateTime.Compare(DateTime.ParseExact(dateQueryParam, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) == 0)
                {
                    foreach (var hourRunning in entry.HoursRunning)
                    {
                        string jsonHour = _formatDateTimeService.GetFormattedHour(hourRunning.Hour);

                        if (DateTime.Compare(DateTime.ParseExact(hourQueryParam, "HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonHour, "HH:mm:ss", CultureInfo.InvariantCulture)) == 0)
                        {
                            return hourRunning.Seats;
                        }
                    }
                }
            }

            return new List<Seats>();
        }

        public int UpdateAvailability(string date, string hour, int movieId, int theatreId, List<Seats> seats)
        {
            string dateQueryParam = _formatDateTimeService.GetFormattedDate(date);
            string hourQueryParam = _formatDateTimeService.GetFormattedHour(hour);

            var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieId && mxt.IdTheatre == theatreId).Select(mxt => mxt.Availability).FirstOrDefault();
            var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
            foreach (var entry in model)
            {
                string jsonDate = _formatDateTimeService.GetFormattedDate(entry.Date);

                if (DateTime.Compare(DateTime.ParseExact(dateQueryParam, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) == 0)
                {
                    foreach (var hourRunning in entry.HoursRunning)
                    {
                        string jsonHour = _formatDateTimeService.GetFormattedHour(hourRunning.Hour);

                        if (DateTime.Compare(DateTime.ParseExact(hourQueryParam, "HH:mm:ss", CultureInfo.InvariantCulture), DateTime.ParseExact(jsonHour, "HH:mm:ss", CultureInfo.InvariantCulture)) == 0)
                        {
                            hourRunning.Seats = seats;
                        }
                    }
                }
            }

            var newAvailabilityJson = JsonConvert.SerializeObject(model);

            try
            {
                _context.MovieXrefTheatres
                    .Where(mxt => mxt.IdMovie == movieId && mxt.IdTheatre == theatreId)
                    .ExecuteUpdate(up => up
                        .SetProperty(mxt => mxt.Availability, newAvailabilityJson)
                    );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieId && mxt.IdTheatre == theatreId).Select(mxt => mxt.IdScreenXrefMovie).FirstOrDefault();
        }

        public List<MovieScheduleAppModel> GetScheduleByMovieId(int movieId)
        {
            List<MovieScheduleAppModel> movieScheduleAppModels = new List<MovieScheduleAppModel>();

            var items = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieId).Select(mxt => new { Availability = mxt.Availability, TheatreId = mxt.IdTheatre }).ToList();

            foreach (var item in items)
            {
                var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(item.Availability);
                foreach (var entry in model)
                {
                    string date = _formatDateTimeService.GetFormattedDate(entry.Date);
                    string currentDate = _formatDateTimeService.GetFormattedDate(DateTime.Now.ToString("d"));
                    if (DateTime.Compare(DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture), DateTime.ParseExact(currentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)) >= 0)
                    {
                        List<string> hours = new List<string>();
                        var dictionaryHours = GetRunningDatesByIdsAndDate(new List<int> { movieId }, date);
                        if (dictionaryHours.Values.Any())
                        {
                            foreach (var hoursRunning in dictionaryHours.Values)
                            {
                                foreach (var hour in hoursRunning)
                                {
                                    if (GetNoOfSeatsAvailable(date, hour, movieId, item.TheatreId) != 0)
                                    {
                                        hours.Add(hour);
                                    }
                                }
                            }
                        }

                        if (hours.Any())
                        {
                            movieScheduleAppModels.Add(new MovieScheduleAppModel
                            {
                                Date = date,
                                TheatreName = item.TheatreId,
                                Hours = hours,
                            });
                        }
                    }
                }
            }

            return movieScheduleAppModels;
        }

        public void DeleteMovieXrefTheatreByMovieId(int movieId)
        {
            var moviesXrefTheatres = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieId).ToList();

            foreach (var movieXrefTheatre in moviesXrefTheatres)
            {
                _context.Bookings.Where(b => b.IdMovieXrefTheatre == movieXrefTheatre.IdScreenXrefMovie).ExecuteDelete();
                _context.MovieXrefTheatres.Where(mxt => mxt.IdScreenXrefMovie == movieXrefTheatre.IdScreenXrefMovie).ExecuteDelete();
            }
        }

        public void DeleteMovieXrefTheatreByTheatreId(int theatreId)
        {
            var moviesXrefTheatres = _context.MovieXrefTheatres.Where(mxt => mxt.IdTheatre == theatreId).ToList();

            foreach (var movieXrefTheatre in moviesXrefTheatres)
            {
                _context.Bookings.Where(b => b.IdMovieXrefTheatre == movieXrefTheatre.IdScreenXrefMovie).ExecuteDelete();
                _context.MovieXrefTheatres.Where(mxt => mxt.IdScreenXrefMovie == movieXrefTheatre.IdScreenXrefMovie).ExecuteDelete();
            }
        }
    }
}
