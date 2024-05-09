using System.ComponentModel.DataAnnotations;

namespace CinemaVillage.AppModel.Movies
{
    public class MovieAddAppModel
    {
        [Required(ErrorMessage = "Please enter the title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the genre")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Please enter the duration")]
        [RegularExpression(@"[0-9]+:[0-9]+", ErrorMessage = "Please enter a format that matches HH:MM")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Please enter the release date")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "Please enter the description of the movie")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please select a director")]
        public string DirectorName { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Please select a theatre")]
        public string TheatreName { get; set; }
    }
}
