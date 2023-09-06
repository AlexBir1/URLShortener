using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Models
{
    public class SettingModel
    {
        public int Id { get; set; }
        public string Account_Id { get; set; }
        public string Key { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
