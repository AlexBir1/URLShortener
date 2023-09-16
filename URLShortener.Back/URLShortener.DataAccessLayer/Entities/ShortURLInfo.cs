using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class ShortURLInfo
    {
        public int Id { get; set; } = 0;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public int URL_Id { get; set; } = 0;
        public ShortURL ShortURL { get; set; } = new ShortURL();
    }
}
