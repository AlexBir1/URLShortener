using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Controllers;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.Repositories;
using URLShortener.Models;

namespace URLShortener.xUnit
{
    public class SettingRepositoryTest
    {
        [Fact]
        public async void GetAll_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.InsertNewSetting(new SettingModel
                {
                    Id = 0,
                    Title = i.ToString(),
                    Description = i.ToString(),
                    Key = i.ToString(),
                    IsActive = false,
                });
            }

            await db.SaveChangesAsync();

            var urls = await _repo.GetAll();

            Assert.Equal(4, urls.Data.Count());
            db.Dispose();
        }

        [Fact]
        public async void GetAllByAccountId_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.InsertNewSetting(new SettingModel
                {
                    Title = i.ToString(),
                    Description = i.ToString(),
                    Key = i.ToString(),
                    IsActive = false,
                });
            }

            await db.SaveChangesAsync();

            for (int i = 0; i < 3; i++)
            {
                await _repo.Insert(new SettingModel
                {
                    Account_Id = "1",
                    Id = i + 2,
                    IsActive = false,
                    
                });
            }

            await db.SaveChangesAsync();

            var urls = await _repo.GetAllByAccountId("1");

            Assert.Equal(4, urls.Data.Count());
            db.Dispose();
        }

        [Fact]
        public async void GetById_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.InsertNewSetting(new SettingModel
                {
                    Title = i.ToString(),
                    Description = i.ToString(),
                    Key = i.ToString(),
                    IsActive = false,
                });
            }

            await db.SaveChangesAsync();

            var url = await _repo.GetById(1);

            Assert.NotNull(url);
            db.Dispose();
        }

        [Fact]
        public async void InsertNewSetting_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

                await _repo.InsertNewSetting(new SettingModel
                {
                    Title = "test",
                    Description = "test",
                    Key = "test",
                    IsActive = false,
                });

            await db.SaveChangesAsync();

            var url = await _repo.GetById(1);

            Assert.NotNull(url);
            db.Dispose();
        }

        [Fact]
        public async void Insert_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            await _repo.InsertNewSetting(new SettingModel
            {
                Title = "test",
                Description = "test",
                Key = "test",
                IsActive = false,
            });

            await db.SaveChangesAsync();

            await _repo.Insert(new SettingModel
            {
                Account_Id = "1",
                Id = 1,
                IsActive = false,

            });

            await db.SaveChangesAsync();

            var urls = await _repo.GetAllByAccountId("1");

            Assert.NotEmpty(urls.Data);
            db.Dispose();
        }
    }
}
