using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.Entities;

namespace URLShortener.DataAccessLayer.JWT
{
    public class JWTTokenProperty
    {
        public string Key { get; set; } = string.Empty;
        public int ExpiresInDays { get; set; }
        public Account? Account { get; set; }

        public JWTTokenProperty(string Key, int expiresInDays, Account Account)
        {
            this.Key = Key;
            this.Account = Account;
            ExpiresInDays = expiresInDays;
        }
    }
}
