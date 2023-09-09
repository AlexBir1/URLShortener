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
    public class SettingControllerTest
    {
        [Fact]
        public async void GetAccountSettings_IfExists()
        {
            var mock = new Mock<IUnitOfWork>();

            var settings = new List<SettingModel>()
            {
                new SettingModel
                {
                    Id = 1,
                    Title = "Title",
                    Description = "Description",
                    Key = "Key",
                    IsActive = true,
                    Account_Id = "accountId",
                },
                new SettingModel
                {
                    Id = 3,
                    Title = "Title",
                    Description = "Description",
                    Key = "Key",
                    IsActive = true,
                    Account_Id = "accountId",
                },
                new SettingModel
                {
                    Id = 2,
                    Title = "Title",
                    Description = "Description",
                    Key = "Key",
                    IsActive = true,
                    Account_Id = "accountId",
                },
            };

            mock.Setup(x => x.Settings.GetAllByAccountId(It.IsAny<string>()))
                .ReturnsAsync(new BaseReponse<IEnumerable<SettingModel>>(settings, null));

            var controller = new SettingController(mock.Object);

            var result = await controller.GetAccountSettings("accountId");
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
            Assert.Equal(3, response.Data.Count());
        }

        [Fact]
        public async void GetAccountSettings_IfNoValue()
        {
            var mock = new Mock<IUnitOfWork>();

            var errors = new string[] { 
                new string("No value")
            };

            mock.Setup(x => x.Settings.GetAllByAccountId(It.IsAny<string>()))
                .ReturnsAsync(new BaseReponse<IEnumerable<SettingModel>>(new SettingModel[0], errors));

            var controller = new SettingController(mock.Object);

            var result = await controller.GetAccountSettings(It.IsAny<string>());
            var response = result.Value;
            Assert.NotNull(result);
            Assert.Empty(response.Data);
        }

        [Fact]
        public async void GetAccountSetting_IfExists()
        {
            var mock = new Mock<IUnitOfWork>();

            var value = new SettingModel
            {
                Id = 1,
                Title = "Title",
                Description = "Description",
                Key = "Key",
                IsActive = true,
                Account_Id = "accountId",
            };

            mock.Setup(x => x.Settings.GetAccountSetting(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new BaseReponse<SettingModel>(value, null));

            var controller = new SettingController(mock.Object);

            var result = await controller.GetAccountSetting(1, "accountId");
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
        }

        [Fact]
        public async void GetAccountSetting_IfNoValue()
        {
            var mock = new Mock<IUnitOfWork>();

            var errors = new List<string>() {
                new string("No value")
            };

            mock.Setup(x => x.Settings.GetAccountSetting(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(new BaseReponse<SettingModel>(null, errors));

            var controller = new SettingController(mock.Object);

            var result = await controller.GetAccountSetting(1, "accountId");
            var response = result.Value;
            Assert.NotNull(result);
            Assert.Null(response.Data);
            Assert.Single(response.Errors);
        }

        [Fact]
        public async void InsertAccountSetting_newValue()
        {
            var mock = new Mock<IUnitOfWork>();

            var settings = new List<SettingModel>()
            {
                new SettingModel
                {
                    Id = 1,
                    Title = "Title",
                    Description = "Description",
                    Key = "Key",
                    IsActive = true,
                    Account_Id = "accountId",
                },
            };

            mock.Setup(x => x.Settings.Insert(It.IsAny<SettingModel>()))
                .ReturnsAsync(new BaseReponse<SettingModel>(settings[0], null));

            var controller = new SettingController(mock.Object);

            var result = await controller.AddAccountSettings(It.IsAny<string>(), settings);
            var response = result.Value;
            Assert.NotNull(result);
            Assert.NotNull(response.Data);
        }
    }
}
