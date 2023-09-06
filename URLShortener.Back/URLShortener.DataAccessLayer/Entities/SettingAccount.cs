using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class SettingAccount
    {
        public int Setting_Id { get; set; }
        public Setting Setting { get; set; }

        public string Account_Id { get; set; }
        public Account Account { get; set; }

        public bool IsActive { get; set; }
    }
}
