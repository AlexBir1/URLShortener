using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.Models
{
    public class SettingModel
    {
        public int Id { get; set; } = 0;
        public string Account_Id { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
