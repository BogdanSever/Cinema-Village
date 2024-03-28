using System.ComponentModel.DataAnnotations;

namespace CinemaVillage.AppModel.Users
{
    public class LogInAppModel
    {
        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com)$", ErrorMessage = "Invalid email type")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }
    }
}
