namespace CinemaVillage.AppModel.Movies
{
    public class MovieProgramPageAppModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Genre { get; set; }

        public string Image { get; set; }

        public List<string> RunningHours { get; set; }

        public int IdTheatre { get; set; }
    }
}
