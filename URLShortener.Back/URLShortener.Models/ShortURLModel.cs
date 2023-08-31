using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class ShortURLModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "URL is required")]
        public string Url { get; set; } = string.Empty;

        public string Origin { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
}
