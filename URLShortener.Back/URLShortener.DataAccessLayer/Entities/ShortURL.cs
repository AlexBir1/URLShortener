using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class ShortURL
    {
        public int Id { get; set; } 
        public string Url { get; set; } 
        public string Origin { get; set; } 
        public string CreatedByUserId { get; set; } 
        public ShortURLInfo Info { get; set; }
    }
}
