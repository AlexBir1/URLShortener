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

        public IAboutContentRepository AboutPageContent { get; private set; }

        public ISettingRepository Settings { get; private set; }

        public bool CanConnect { get; private set; }

        public AppDBContext DatabaseContext { get; private set; }

        public async Task CommitAsync()
        {
            try
            {
                await DatabaseContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {

            }
        }

        public UnitOfWork(AppDBContext db, UserManager<Account> userManager, JWTService jwtService, IConfiguration config, SignInManager<Account> signInManager)
        {
            DatabaseContext = db;
            CanConnect = DatabaseContext.Database.CanConnect();

            Settings = new SettingRepository(db);
            ShortURLs = new ShortURLRepository(db);
            Accounts = new AccountRepository(userManager, config, jwtService, signInManager);
            AboutPageContent = new AboutContentRepository(db);
        }
    }
}
