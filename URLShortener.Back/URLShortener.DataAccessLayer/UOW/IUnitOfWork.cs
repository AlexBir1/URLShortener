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

namespace URLShortener.DataAccessLayer.UOW
{
    public interface IUnitOfWork
    {
        Task CommitAsync();

        IAboutContentRepository AboutPageContent { get; }

        IAccountRepository Accounts { get; }

        IShortURLRepository ShortURLs { get; }

        ISettingRepository Settings { get; }

        bool CanConnect { get; }

        AppDBContext DatabaseContext { get; }

    }
}
