using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class SettingAccount
    {
        public int Setting_Id { get; set; } = 0;
        public Setting Setting { get; set; } = new Setting();

        public string Account_Id { get; set; } = string.Empty;
        public Account Account { get; set; } = new Account();

        public bool IsActive { get; set; } = false;
    }
}
