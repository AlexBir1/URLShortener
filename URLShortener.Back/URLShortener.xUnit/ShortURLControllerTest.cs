
using Microsoft.AspNetCore.Mvc;
using Moq;
using URLShortener.Controllers;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;

namespace URLShortener.xUnit
{
    public class ShortURLControllerTest
    {

        [Fact]
        public async void Insert_NewValue()
        {
            var mock = new Mock<IUnitOfWork>();
            var mockDB = new Mock<AppDBContext>();

            var newUrl = new ShortURL
            {
                Id = 0,
                Url = "newurl",
                Origin = "newurl.origin",
                CreatedByUserId = "userid",
            };

            var insertResponse = new BaseReponse<ShortURL>(newUrl, null);

            mock.Setup(x => x.ShortURLs.Insert(It.IsAny<ShortURL>())).ReturnsAsync(insertResponse);

            var controller = new ShortURLController(mock.Object);

            var model = new ShortURLModel
            {
                Id = 0,
                Url = "newurl",
                Origin = "newurl.origin",
                CreatedByUserId = "userid",
                CreatedBy = "username"
            };

            var result = await controller.AddUrl(model);
            var response = result.Value;
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetAll_IfExists()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ShortURLs.GetAll()).ReturnsAsync(new BaseReponse<IEnumerable<ShortURL>>(GetTextUrls(), null));

            var controller = new ShortURLController(mock.Object);

            var result = await controller.GetUrls();
            var response = result.Value;
            Assert.NotNull(result);
            Assert.IsType<List<ShortURLModel>>(response.Data);
            Assert.Equal(2, response.Data.Count());
        }

        private List<ShortURL> GetTextUrls() 
        {
            return new List<ShortURL>
            {
                new ShortURL
                {
                    Id=1,
                    Url="http://shortUrl1.com",
                },
                new ShortURL
                {
                    Id=2,
                    Url="http://shortUrl2.ua",
                },
            };
        }

        [Fact]
        public async void GetAll_ZeroCount()
        {
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ShortURLs.GetAll()).ReturnsAsync(new BaseReponse<IEnumerable<ShortURL>>(new List<ShortURL>(), null));

            var controller = new ShortURLController(mock.Object);

            var result = await controller.GetUrls();
            var response = result.Value;
            Assert.NotNull(result);
            Assert.IsType<List<ShortURLModel>>(response.Data);
            Assert.Empty(response.Data);
        }

        [Fact]
        public async void GetAll_Errors()
        {
            List<string> errors = new List<string>()
            {
                new string("Error-1"),
                new string("Error-2"),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ShortURLs.GetAll()).ReturnsAsync(new BaseReponse<IEnumerable<ShortURL>>(null, errors));

            var controller = new ShortURLController(mock.Object);

            var result = await controller.GetUrls();
            var response = result.Value;
            Assert.NotNull(result);
            Assert.Null(response.Data);
            Assert.NotNull(response.Errors);
            Assert.Equal(2, response.Errors.Count());
        }

        [Fact]
        public async void GetById_IfValueExists()
        {
            var url = new ShortURL
            {
                Id = 1,
                Url = "http://shortUrl1.shortened.com",
                Origin = "http://shortUrl1.com",
                Info = new ShortURLInfo
                {
                    Id = 1,
                    URL_Id = 1,
                    CreatedBy = "User",
                    CreationDate = DateTime.Now,
                }
            };
            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ShortURLs.GetById(1)).ReturnsAsync(new BaseReponse<ShortURL>(url, null));

            var controller = new ShortURLController(mock.Object);

            var result = await controller.GetUrl(1);
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
            Assert.Equal(1, response.Data.Id);
        }

        [Fact]
        public async void GetById_IfNoValue()
        {
            List<string> errors = new List<string>()
            {
                new string("Error-1"),
                new string("Error-2"),
            };

            var mock = new Mock<IUnitOfWork>();
            mock.Setup(x => x.ShortURLs.GetById(1)).ReturnsAsync(new BaseReponse<ShortURL>(null, errors));

            var controller = new ShortURLController(mock.Object);

            var result = await controller.GetUrl(1);
            var response = result.Value;
            Assert.NotNull(result);
            Assert.Null(response.Data);
            Assert.NotNull(response.Errors);
            Assert.Equal(2, response.Errors.Count());
        }
    }
}
