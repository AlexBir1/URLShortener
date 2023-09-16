using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class ShortURL
    {
        public int Id { get; set; } = 0;
        public string Url { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string CreatedByUserId { get; set; } = string.Empty;
        public ShortURLInfo Info { get; set; } = new ShortURLInfo();
    }
}
