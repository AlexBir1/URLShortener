namespace URLShortener.Models
{
    public class ShortURLInfoModel
    {
        public int Id { get; set; } 
        public string Url { get; set; } 
        public string Origin { get; set; } 
        public string CreatedBy { get; set; } 
        public string CreatedByUserId { get; set; } 
        public DateTime CreationDate { get; set; } 
    }
}
