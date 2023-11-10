using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;

namespace URLShortener.nUnit
{
    [TestFixture]
    public class SettingControllerTest
    {
        [Test]
        public async Task GetAccountSettings_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<IEnumerable<Setting>>(new List<Setting>() {
                        new Setting()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            SettingAccounts = new List<SettingAccount>
                            {
                                new SettingAccount()
                            {
                                Account_Id = "test",
                                IsActive = false,
                            }
                            }

                        },
                        new Setting()
                        {
                            Id = 1,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            SettingAccounts = new List<SettingAccount>
                            {
                                new SettingAccount()
                            {
                                Account_Id = "test",
                                IsActive = false,
                            }
                            }
                        }
                    }, null);

                    uowMock.Setup(x => x.Settings.GetAllByAccountId(It.IsAny<string>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);

                });
            });

            HttpClient client = webApp.CreateClient();

            var response = await client.GetAsync("api/Setting/ghjguiyi");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<IEnumerable<SettingModel>>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task GetAccountSetting_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<Setting>(
                        new Setting()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            SettingAccounts = new List<SettingAccount>
                            {
                                new SettingAccount()
                            {
                                Account_Id = "test",
                                IsActive = false,
                            }
                            }
                        }, null);

                    uowMock.Setup(x => x.Settings.GetAccountSetting(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);

                });
            });

            HttpClient client = webApp.CreateClient();

            var response = await client.GetAsync("api/Setting/1/fdsfdsfsd");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<SettingModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task AddAccountSettings_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<Setting>(
                        new Setting()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            SettingAccounts = new List<SettingAccount>
                            {
                                new SettingAccount()
                            {
                                Account_Id = "test",
                                IsActive = false,
                            }
                            }
                        }, null);

                    uowMock.Setup(x => x.Settings.Insert(It.IsAny<Setting>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);

                });
            });

            HttpClient client = webApp.CreateClient();

            var model = new SettingModel[] {
                        new SettingModel()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            Account_Id = "test",
                            IsActive = false,
                        },
                        new SettingModel()
                        {
                            Id = 1,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            Account_Id = "test",
                            IsActive = false,
                        }
                    };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/Setting/dasdsad", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<IEnumerable<SettingModel>>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task UpdateAccountSettings_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<Setting>(
                        new Setting()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            SettingAccounts = new List<SettingAccount>
                            {
                                new SettingAccount()
                            {
                                Account_Id = "test",
                                IsActive = false,
                            }
                            }
                        }, null);

                    uowMock.Setup(x => x.Settings.Update(It.IsAny<int>(), It.IsAny<Setting>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);

                });
            });

            HttpClient client = webApp.CreateClient();

            var model = new List<SettingModel> {
                        new SettingModel()
                        {
                            Id = 44,
                            Title = "test",
                            Description = "test",
                            Key = "test",
                            Account_Id = "test",
                            IsActive = false,
                        }
                    };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PatchAsync("api/Setting/dasdsad", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<IEnumerable<SettingModel>>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }
    }
}
