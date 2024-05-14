using System.Drawing;

namespace CinemaVillage.AppModel.Movies
{
    public class MovieAppModel
    {
        public int IdMovie { get; set; }
        public string Title { get; set; }

        public string Genre { get; set; }

        public string Duration { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public int IdDirector { get; set; }
    }
}
