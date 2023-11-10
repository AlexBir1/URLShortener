using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.Models;

namespace URLShortener.Services.Interfaces
{
    public interface ISettingService
    {
        public Task<IBaseResponse<IEnumerable<SettingModel>>> InsertUserSettings(string accountId, IEnumerable<SettingModel> settings);
        public Task<IBaseResponse<SettingModel>> InsertGlobalSetting(SettingModel model);

        public Task<IBaseResponse<SettingModel>> DeleteUserSetting(int settingId, string userId);
        public Task<IBaseResponse<SettingModel>> DeleteGlobalSetting(int settingId);

        public Task<IBaseResponse<IEnumerable<SettingModel>>> UpdateUserSettings(string accountId, IEnumerable<SettingModel> settings);
        public Task<IBaseResponse<SettingModel>> UpdateGlobalSetting(int id, SettingModel model);

        public Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllGlobalSettings();
        public Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllUserSettings(string userId);

        public Task<IBaseResponse<SettingModel>> GetGlobalSettingById(int id);
        public Task<IBaseResponse<SettingModel>> GetUserSettingById(string userId, int settingId);
    }
}
