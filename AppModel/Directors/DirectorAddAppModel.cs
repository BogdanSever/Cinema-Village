using System.ComponentModel.DataAnnotations;

namespace CinemaVillage.AppModel.Directors
{
    public class DirectorAddAppModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a valid first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a valid last name")]
        public string LastName { get; set; }
    }
}
