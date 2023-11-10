using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace URLShortener.Shortener
{
    public static class ShortURLMaker
    {
        private static string GetShortenedUrlPath(long key) =>
            WebEncoders.Base64UrlEncode(BitConverter.GetBytes(key));

        public static string ShortenURL(string value, string createdByUsername, string baseUrl = "http://localhost:4200/")
        {
            byte[] urlBytes = Encoding.ASCII.GetBytes(value);
            byte[] usernameBytes = Encoding.ASCII.GetBytes(createdByUsername);

            long urlAndUsernameBytesSum = 0;

            foreach (byte b in urlBytes)
            {
                urlAndUsernameBytesSum += b;
            }

            foreach (byte b in usernameBytes)
            {
                urlAndUsernameBytesSum += b;
            }

            var key = DateTime.UtcNow.Ticks + urlAndUsernameBytesSum;

            var generatedUrl = baseUrl + GetShortenedUrlPath(key);

            return generatedUrl;
        }
    }
}
