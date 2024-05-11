using System.Collections;
using System.Text.Json.Serialization;

namespace CinemaVillage.AppModel.Movies
{
    public class MovieAddJsonAppModel
    {
        public string Date { get; set; }
        public List<HoursRunning> HoursRunning { get; set; }
        
    }

    public class HoursRunning
    {
        public string Hour { get; set; }
        public List<Seats> Seats { get; set; }
    }

    public class Seats
    {
        public int SeatId { get; set; }
        public bool Available { get; set; }
    }
}
