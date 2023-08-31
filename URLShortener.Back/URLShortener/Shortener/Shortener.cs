using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace URLShortener.Shortener
{
    public static class Shortener
    {
        public static string GetUrlChunk(this long key) =>
            WebEncoders.Base64UrlEncode(BitConverter.GetBytes(key));

        public static string shorten(this string value)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(value);
            long temp = 0;
            foreach (var item in asciiBytes)
                temp += item;
            // key = Tiks + sub of string bytes: that will be always unique
            var key = DateTime.UtcNow.Ticks + temp;
            var generatedUrl = "http://localhost:4200/" + key.GetUrlChunk();
            return generatedUrl;
        }
    }
}
