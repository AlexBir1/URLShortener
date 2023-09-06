using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.DataAccessLayer.Repositories;

namespace URLShortener.DataAccessLayer.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAccountRepository Accounts { get; private set; }

        public IShortURLRepository ShortURLs { get; private set; }

        public IConfiguration _config { get; private set; }

        public bool CanConnect { get; private set; }

        public AppDBContext _db { get; private set; }

        public UserManager<Account> _userManager { get; private set; }

        public JWTService _jwtService { get; private set; }

        public SignInManager<Account> SignInManager { get; private set; }

        public ISettingRepository Settings { get; private set; }

        public async Task CommitAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public UnitOfWork(AppDBContext db, UserManager<Account> userManager, JWTService jwtService, IConfiguration config, SignInManager<Account> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _jwtService = jwtService;
            _config = config;
            SignInManager = signInManager;
            CanConnect = _db.Database.CanConnect();
            Accounts = new AccountRepository(_userManager, _config, _jwtService, SignInManager);
            ShortURLs = new ShortURLRepository(_db);
            Settings = new SettingRepository(_db);

        }
    }
}
