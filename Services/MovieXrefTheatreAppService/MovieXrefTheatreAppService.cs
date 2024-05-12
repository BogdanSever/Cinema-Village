using CinemaVillage.AppModel.Movies;
using CinemaVillage.DatabaseContext;
using CinemaVillage.Models;
using CinemaVillage.Services.MovieXrefTheatreAppService.Interface;
using Newtonsoft.Json;

namespace CinemaVillage.Services.MovieXrefTheatreAppService
{
    public class MovieXrefTheatreAppService : IMovieXrefTheatreAppService
    {
        private readonly CinemaDbContext _context;

        public MovieXrefTheatreAppService(CinemaDbContext context)
        {
            _context = context;
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
            Dictionary<int, List<string>> dictDatesAndHours = new();

            foreach (int id in moviesIds)
            {
                var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == id).Select(mxt => mxt.Availability).FirstOrDefault();
                var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
                foreach (var entry in model)
                {
                    if (DateOnly.Parse(entry.Date) == DateOnly.Parse(date))
                    {
                        var hours = new List<string>();
                        foreach (var hourRunning in entry.HoursRunning)
                        {
                            if (DateOnly.Parse(date) == DateOnly.FromDateTime(DateTime.Now))
                            {
                                if (!(TimeOnly.Parse(hourRunning.Hour) < TimeOnly.FromDateTime(DateTime.Now)))
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

            return dictDatesAndHours;
        }

        public int GetNoOfSeatsAvailable(string date, string hour, int movieID, int theatreID)
        {
            int noOfSeatsAvailable = 0;

            var availability = _context.MovieXrefTheatres.Where(mxt => mxt.IdMovie == movieID && mxt.IdTheatre == theatreID).Select(mxt => mxt.Availability).FirstOrDefault();
            var model = JsonConvert.DeserializeObject<List<MovieAddJsonAppModel>>(availability);
            foreach (var entry in model)
            {
                if(DateOnly.Parse(date) == DateOnly.Parse(entry.Date))
                {
                    foreach (var hourRunning in entry.HoursRunning)
                    {
                        if (TimeOnly.Parse(hourRunning.Hour) == TimeOnly.Parse(hour))
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
    }
}
