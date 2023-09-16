using System.ComponentModel.DataAnnotations;

namespace URLShortener.Models
{
    public class ShortURLModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "URL is required")]
        public string Url { get; set; } 

        public string Origin { get; set; } 
        public string CreatedBy { get; set; } 
        public string CreatedByUserId { get; set; } 
    }
}
