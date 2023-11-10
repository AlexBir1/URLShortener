using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.Models;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface ISettingRepository : IRepository<Setting>
    {
        public Task<IBaseResponse<IEnumerable<Setting>>> GetAllByAccountId(string accountId);
        public Task<IBaseResponse<Setting>> GetAccountSetting(int settingId, string accountId);
        public Task<IBaseResponse<Setting>> InsertGlobalSetting(Setting model);
        public Task<IBaseResponse<Setting>> UpdateGlobalSetting(int id, Setting model);
    }
}
