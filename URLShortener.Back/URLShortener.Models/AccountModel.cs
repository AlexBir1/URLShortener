namespace URLShortener.Models
{
    public class AccountModel
    {
        public string Id { get; set; }
        public string Username { get; set; } 
        public string JWTToken { get; set; } 
        public string Role { get; set; } 
    }
}
