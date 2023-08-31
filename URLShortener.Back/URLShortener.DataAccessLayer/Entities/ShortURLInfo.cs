using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class ShortURLInfo
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        public int URL_Id { get; set; }
        public ShortURL ShortURL { get; set; }
    }
}
