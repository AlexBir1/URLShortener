namespace URLShortener.Models
{
    public class ShortURLInfoModel
    {
        public int Id { get; set; } = 0;
        public string Url { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
