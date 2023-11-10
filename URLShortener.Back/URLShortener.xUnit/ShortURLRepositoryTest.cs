using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.Repositories;
using Xunit;

namespace URLShortener.xUnit
{
    public class ShortURLRepositoryTest
    {
        [Fact]
        public async void GetAll_IfExists()
        {
            //Arrange
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            IShortURLRepository _repo = new ShortURLRepository(db);

            for (int i = 0; i < 3; i++)
            {
                await _repo.Insert(new ShortURL()
                {
                    Url = "test" + i,
                    Origin = "test" + i,
                    CreatedByUserId = "asdasdas" + i,
                    Info = new ShortURLInfo()
                    {
                        CreatedBy = "dasdsa" + i,
                        CreationDate = DateTime.Now,
                    }
                });
            }

            await db.SaveChangesAsync();
            //Act
            var urls = await _repo.GetAll();
            //Assert
            Assert.Equal(3, urls.Data.Count());

            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void GetById_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            IShortURLRepository _repo = new ShortURLRepository(db);

            await _repo.Insert(new ShortURL()
            {
                Url = "test",
                Origin = "test",
                CreatedByUserId = "asdasdas",
                Info = new ShortURLInfo()
                {
                    CreatedBy = "dasdsa",
                    CreationDate = DateTime.Now,
                }
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

            IShortURLRepository _repo = new ShortURLRepository(db);

            await _repo.Insert(new ShortURL()
            {
                Url = "test",
                Origin = "test",
                CreatedByUserId = "asdasdas",
                Info = new ShortURLInfo()
                {
                    CreatedBy = "dasdsa",
                    CreationDate = DateTime.Now,
                }
            });

            await db.SaveChangesAsync();

            var urls = await _repo.GetAll();

            Assert.NotNull(urls.Data);
            Assert.NotEmpty(urls.Data);

            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }

        [Fact]
        public async void Delete_IfExists()
        {
            AppDBContext db = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase("URLShortenerTestDB").Options);

            IShortURLRepository _repo = new ShortURLRepository(db);

            await _repo.Insert(new ShortURL()
            {
                Url = "test",
                Origin = "test",
                CreatedByUserId = "asdasdas",
                Info = new ShortURLInfo()
                {
                    CreatedBy = "dasdsa",
                    CreationDate = DateTime.Now,
                }
            });

            await db.SaveChangesAsync();

            var urls = await _repo.Delete(1);

            Assert.NotNull(urls.Data);

            await db.Database.EnsureDeletedAsync();
            await db.DisposeAsync();
        }
    }
}
