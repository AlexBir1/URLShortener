using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.Models;

namespace URLShortener.nUnit
{
    [TestFixture]
    public class ShortURLControllerTest
    {
        [Test]
        public async Task GetAll_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<IEnumerable<ShortURL>>(new List<ShortURL>()
                    {
                        new ShortURL
                        {
                            Id = 1,
                            Url = "shortenedURL",
                            Origin = "originURL"
                        },
                        new ShortURL
                        {
                            Id = 2,
                            Url = "shortenedURL",
                            Origin = "originURL"
                        },
                    }, null);

                    uowMock.Setup(x => x.ShortURLs.GetAll()).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var response = await client.GetAsync("api/ShortURL");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<IEnumerable<ShortURLModel>>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetById_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<ShortURL>(new ShortURL()
                    {

                            Id = 1,
                            Url = "shortenedURL",
                            Origin = "originURL",
                            Info = new ShortURLInfo
                            {
                                CreatedBy = "test",
                                CreationDate = DateTime.Now
                            },
                    }, null);

                    uowMock.Setup(x => x.ShortURLs.GetById(It.IsAny<int>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var response = await client.GetAsync("api/ShortURL/1");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<ShortURLInfoModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task Insert_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<ShortURL>(new ShortURL()
                    {

                        Id = 1,
                        Url = "shortenedURL",
                        Origin = "originURL",
                        Info = new ShortURLInfo
                        {
                            CreatedBy = "test",
                            CreationDate = DateTime.Now
                        },
                    }, null);

                    uowMock.Setup(x => x.ShortURLs.Insert(It.IsAny<ShortURL>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var model = new ShortURLModel
            {
                Id = 0,
                Url = "shortenedURL",
                Origin = "originURL",
                CreatedBy = "test",
                CreatedByUserId = "test"
            };

            var account = new Account()
            {
                UserName = "Test",
            };

            var userStoreMock = new Mock<IUserStore<Account>>();
            var userManagerMock = new Mock<UserManager<Account>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(false);

            var config = InitConfiguration();
            var token = new JWTService(userManagerMock.Object).CreateToken(new JWTTokenProperty(config["JWT:Key"], 15, account));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Result);

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/ShortURL", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<ShortURLModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task Delete_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<ShortURL>(new ShortURL()
                    {

                        Id = 1,
                        Url = "shortenedURL",
                        Origin = "originURL",
                        Info = new ShortURLInfo
                        {
                            CreatedBy = "test",
                            CreationDate = DateTime.Now
                        },
                    }, null);

                    uowMock.Setup(x => x.ShortURLs.Delete(It.IsAny<int>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            var account = new Account()
            {
                UserName = "Test",
            };

            var userStoreMock = new Mock<IUserStore<Account>>();
            var userManagerMock = new Mock<UserManager<Account>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(false);

            var config = InitConfiguration();
            var token = new JWTService(userManagerMock.Object).CreateToken(new JWTTokenProperty(config["JWT:Key"], 15, account));

            HttpClient client = webApp.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Result);

            var response = await client.DeleteAsync("api/ShortURL/1");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<ShortURLModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task TryRedirect_ShouldReturnOriginURL()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<ShortURL>(new ShortURL()
                    {

                        Id = 1,
                        Url = "shortenedURL",
                        Origin = "originURL",
                        Info = new ShortURLInfo
                        {
                            CreatedBy = "test",
                            CreationDate = DateTime.Now
                        },
                    }, null);

                    uowMock.Setup(x => x.ShortURLs.GetOriginByShortenURLPathname(It.IsAny<string>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var response = await client.GetAsync("api/ShortURL/TryRedirect/sadsadsa");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<string>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}
