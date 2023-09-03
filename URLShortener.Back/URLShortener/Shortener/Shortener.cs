using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace URLShortener.Shortener
{
    public static class Shortener
    {
        private static string GetUrlPath(this long key) =>
            WebEncoders.Base64UrlEncode(BitConverter.GetBytes(key));

        public static string ShortenURL(this string value, string createdByUsername)
        {
            byte[] urlBytes = Encoding.ASCII.GetBytes(value);
            byte[] usernameBytes = Encoding.ASCII.GetBytes(createdByUsername);
            long temp = 0;
            foreach (byte b in urlBytes)
            {
                temp += b;
            }
            foreach (byte b in usernameBytes)
            {
                temp += b;
            }
            var key = DateTime.UtcNow.Ticks + temp;
            var generatedUrl = "http://localhost:4200/" + key.GetUrlPath();
            return generatedUrl;
        }
    }
}
