using CinemaVillage.Customs.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CinemaVillage.AppModel.Users
{
    public class SignUpAppModel
    {
        [Required(ErrorMessage = "Please enter a valid first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a valid last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter a valid email")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com)$", ErrorMessage = "Invalid email type")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your password again")]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string RepeatPassword { get; set; }

    }
}
