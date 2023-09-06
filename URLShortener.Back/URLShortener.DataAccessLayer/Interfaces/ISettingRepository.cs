using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.Models;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface ISettingRepository : IRepository<SettingModel>
    {
        public Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllByAccountId(string accountId);
        public Task<IBaseResponse<SettingModel>> GetAccountSetting(int settingId, string accountId);
        public Task<IBaseResponse<SettingModel>> InsertNewSetting(SettingModel model);
        public Task<IBaseResponse<SettingModel>> UpdateSetting(int id, SettingModel model);
    }
}
