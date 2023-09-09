using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.Controllers;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.Models;

namespace URLShortener.xUnit
{
    public class AccountControllerTest
    {
        [Fact]
        public async void SignIn_AccountExists() 
        {
            var mock = new Mock<IUnitOfWork>();

            var account = new AccountModel
            {
                Id = "Test",
                Username = "Test",
                Role = "Test",
                JWTToken = "Test",
            };

            mock.Setup(x => x.Accounts.SignIn(It.IsAny<SignInModel>()))
                .ReturnsAsync(new BaseReponse<AccountModel>(account, null));

            var controller = new AccountController(mock.Object);

            var result = await controller.SignIn(It.IsAny<SignInModel>());
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
            Assert.Null(response.Errors);
        }

        [Fact]
        public async void SignUp_NewAccount()
        {
            var mock = new Mock<IUnitOfWork>();

            var account = new AccountModel
            {
                Id = "Test",
                Username = "Test",
                Role = "Test",
                JWTToken = "Test",
            };

            mock.Setup(x => x.Accounts.SignUp(It.IsAny<SignUpModel>()))
                .ReturnsAsync(new BaseReponse<AccountModel>(account, null));

            var controller = new AccountController(mock.Object);

            var result = await controller.SignUp(It.IsAny<SignUpModel>());
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
            Assert.Null(response.Errors);
        }
    }
}
