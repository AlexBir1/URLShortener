using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.JWT;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        IAccountRepository Accounts { get; }

        IShortURLRepository ShortURLs { get; }

        ISettingRepository Settings { get; }

        IConfiguration _config { get; }

        bool CanConnect { get; }

        AppDBContext _db { get; }

        UserManager<Account> _userManager { get; }

        JWTService _jwtService { get; }

    }
}
