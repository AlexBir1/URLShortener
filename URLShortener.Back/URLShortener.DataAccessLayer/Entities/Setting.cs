using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class Setting
    {
        public int Id { get; set; } = 0;
        public string Key { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<SettingAccount> SettingAccounts { get; set; } = new List<SettingAccount>();
    }
}
