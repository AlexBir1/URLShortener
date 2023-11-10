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

            List<Setting> expected = new List<Setting>() 
            {
                new Setting
                {
                    Title = "Title",
                    Key = "Key",
                    Description = "Description",
                },
                new Setting
                {
                    Title = "Title",
                    Key = "Key",
                    Description = "Description",
                },
                new Setting
                {
                    Title = "Title",
                    Key = "Key",
                    Description = "Description",
                }
            };

            foreach(Setting item in expected)
            {
                await db.Settings.AddRangeAsync(item);
            }

            await db.SaveChangesAsync();

            var urls = await _repo.GetAll();

            Assert.Equal(expected.Count, urls.Data.Count());

            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void GetAllByAccountId_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.InsertGlobalSetting(new Setting
                {
                    Title = i.ToString(),
                    Description = i.ToString(),
                    Key = i.ToString(),
                });
            }

            await db.SaveChangesAsync();

            for (int i = 0; i < 3; i++)
            {
                await _repo.Insert(new Setting
                {
                    Id = i + 2,
                    SettingAccounts = new List<SettingAccount>
                    {
                        new SettingAccount
                        {
                            Setting_Id = i + 2,
                            Account_Id = "1",
                            IsActive = false,
                        }
                    }
                });
            }

            await db.SaveChangesAsync();

            var urls = await _repo.GetAllByAccountId("1");

            Assert.Equal(2, urls.Data.Count());
            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void GetById_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.InsertGlobalSetting(new Setting
                {
                    Title = i.ToString(),
                    Description = i.ToString(),
                    Key = i.ToString(),

                });
            }

            await db.SaveChangesAsync();

            var url = await _repo.GetById(1);

            Assert.NotNull(url);
            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void InsertNewSetting_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

                await _repo.InsertGlobalSetting(new Setting
                {
                    Title = "test",
                    Description = "test",
                    Key = "test",
                });

            await db.SaveChangesAsync();

            var url = await _repo.GetById(1);

            Assert.NotNull(url);
            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void Insert_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            ISettingRepository _repo = new SettingRepository(db);

            await _repo.InsertGlobalSetting(new Setting
            {
                Title = "test",
                Description = "test",
                Key = "test",
            });

            await db.SaveChangesAsync();

            await _repo.Insert(new Setting
            {
                
                Id = 1,
                SettingAccounts = new List<SettingAccount>
                    {
                        new SettingAccount
                        {
                            Setting_Id = 1,
                            Account_Id = "1",
                            IsActive = false,
                        }
                    }

            });

            await db.SaveChangesAsync();

            var urls = await _repo.GetAllByAccountId("1");

            Assert.NotEmpty(urls.Data);
            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }
    }
}
