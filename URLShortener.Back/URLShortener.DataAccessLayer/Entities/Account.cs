using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLShortener.DataAccessLayer.Entities
{
    public class Account : IdentityUser
    {
        public virtual ICollection<SettingAccount> AccountSettings { get; set; }
    }
}
