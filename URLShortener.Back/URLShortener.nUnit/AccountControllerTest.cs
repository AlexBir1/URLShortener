using Azure.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.Models;

namespace URLShortener.nUnit
{
    [TestFixture]
    public class AccountControllerTest
    {
        [Test]
        public async Task SignUp_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<AccountModel>(new AccountModel()
                    {
                        Username = "Test",
                    }, null);


                    uowMock.Setup(x => x.Accounts.SignUp(It.IsAny<SignUpModel>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var model = new SignUpModel
            {
                Username = "asdsa",
                Password = "asdfghjklzxcvbbbbbb",
                PasswordConfirm = "asdfghjklzxcvbbbbbb"
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/account/SignUp", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<AccountModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task SignIn_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<AccountModel>(new AccountModel()
                    {
                        Username = "Test",
                    }, null);


                    uowMock.Setup(x => x.Accounts.SignIn(It.IsAny<SignInModel>())).ReturnsAsync(response);

                    services.AddTransient(_ => uowMock.Object);
                });
            });

            HttpClient client = webApp.CreateClient();

            var model = new SignInModel
            {
                Login = "asdsa",
                Password = "asdfghjklzxcvbbbbbb",
            };

            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/account/SignIn", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<AccountModel>>(stringResponse);

            Assert.NotNull(baseResponse);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task Update_ShouldReturnBaseResponse()
        {
            WebApplicationFactory<Program> webApp = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    var uow = services.FirstOrDefault(x => x.ServiceType == typeof(IUnitOfWork));
                    services.Remove(uow);
                    var uowMock = new Mock<IUnitOfWork>();

                    var response = new BaseReponse<Account>(new Account()
                    {
                        UserName = "Test",
                    }, null);

                    var responseModel = new BaseReponse<AccountModel>(new AccountModel()
                    {
                        Username = "Test",
                    }, null);

                    uowMock.Setup(x => x.Accounts.Update(It.IsAny<string>(), It.IsAny<Account>(), It.IsAny<ChangePasswordProperties>())).ReturnsAsync(response);
                    uowMock.Setup(x => x.Accounts.RefreshJWT(It.IsAny<string>())).ReturnsAsync(responseModel);

                    services.AddTransient(_ => uowMock.Object);
                    
                });
            });

            var userStoreMock = new Mock<IUserStore<Account>>();
            var userManagerMock = new Mock<UserManager<Account>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

            userManagerMock.Setup(x => x.IsInRoleAsync(It.IsAny<Account>(), It.IsAny<string>())).ReturnsAsync(false);

            HttpClient client = webApp.CreateClient();

            var model = new UpdateAccountModel
            {
                Username = "Test",
                Id = "Test",
                OldPassword = "Test",
                NewPassword = "TestPass",
                ConfirmNewPassword = "TestPass",
            };

            var account = new Account()
            {
                UserName = "Test",
            };

            var config = InitConfiguration();
            var token = new JWTService(userManagerMock.Object).CreateToken(new JWTTokenProperty(config["JWT:Key"], 15, account));

            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.Result);
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/account/" + account.Id, content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var baseResponse = JsonConvert.DeserializeObject<BaseReponse<AccountModel>>(stringResponse);

            Assert.IsNotNull(baseResponse);
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
