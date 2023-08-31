﻿namespace URLShortener.Models
{
    public class AccountModel
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string JWTToken { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
