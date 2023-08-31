using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password confirmation is required")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string PasswordConfirm { get; set; } = string.Empty;
    }
}
