using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.DBContext;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.Models;

namespace URLShortener.DataAccessLayer.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly AppDBContext _db;

        public SettingRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<IBaseResponse<Setting>> Delete(int id)
        {
            try
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(x=>x.Id == id);
                if (setting == null)
                {
                    var errors = new List<string>()
                    {
                        new string("No settings were found")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }
                _db.Settings.Remove(setting);

                return new BaseReponse<Setting>(setting, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> GetAccountSetting(int settingId, string accountId)
        {
            try
            {
                var setting = await _db.SettingsAccounts.Include(x=>x.Setting)
                    .FirstOrDefaultAsync(x => x.Setting_Id == settingId && x.Account_Id == accountId);

                if (setting == null)
                {
                    var errors = new List<string>()
                    {
                        new string("No settings were found")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }

                var userSetting = new Setting
                {
                    Id = setting.Setting_Id,
                    Key = setting.Setting.Key,
                    Title = setting.Setting.Title,
                    Description = setting.Setting.Description,
                    SettingAccounts = new List<SettingAccount>()
                    {
                        new SettingAccount
                        {
                            Setting_Id = setting.Setting_Id,
                            Account_Id = setting.Account_Id,
                            IsActive = setting.IsActive
                        }
                    }
                };

                return new BaseReponse<Setting>(userSetting, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<Setting>>> GetAll()
        {
            try
            {
                var settings = await _db.Settings.ToListAsync();
                if (settings == null)
                {
                    var errors = new List<string>()
                    {
                        new string("No settings were found")
                    };
                    return new BaseReponse<IEnumerable<Setting>>(null, errors);
                }

                return new BaseReponse<IEnumerable<Setting>>(settings, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<Setting>>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<Setting>>> GetAllByAccountId(string accountId)
        {
            try
            {
                var settingsAccounts = await _db.SettingsAccounts.Include(x => x.Setting)
                    .Where(x => x.Account_Id == accountId).ToListAsync();
                if (settingsAccounts.Count == 0)
                {
                    var errors = new List<string>()
                    {
                        new string("No settings were found")
                    };
                    return new BaseReponse<IEnumerable<Setting>>(null, errors);
                }

                var settings = settingsAccounts.Select(x => new Setting
                {
                    Id = x.Setting.Id,
                    Key = x.Setting.Key,
                    Title = x.Setting.Title,
                    Description = x.Setting.Description,
                    SettingAccounts = new List<SettingAccount>()
                    {
                        new SettingAccount
                        {
                            Setting_Id = x.Setting_Id,
                            Account_Id = x.Account_Id,
                            IsActive = x.IsActive
                        }
                    }
                }).ToList();

                return new BaseReponse<IEnumerable<Setting>>(settings, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<Setting>>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> GetById(int id)
        {
            try
            {
                var setting = await _db.Settings.Include(x=>x.SettingAccounts)
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (setting == null)
                {
                    var errors = new List<string>()
                    {
                        new string("This setting does not exist")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }

                return new BaseReponse<Setting>(setting, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> Insert(Setting Entity)
        {
            try
            {
                var settingAccount = new SettingAccount
                {
                    Account_Id = Entity.SettingAccounts.First().Account_Id,
                    Setting_Id = Entity.Id,
                    IsActive = Entity.SettingAccounts.First().IsActive,
                };

                await _db.SettingsAccounts.AddAsync(settingAccount);

                return new BaseReponse<Setting>(Entity, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> InsertGlobalSetting(Setting model)
        {
            try
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(x => x.Key == model.Key);
                if(setting != null)
                {
                    var errors = new List<string>()
                    {
                        new string("This setting exists")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }

                var newSetting = await _db.Settings.AddAsync(model);

                return new BaseReponse<Setting>(newSetting.Entity, null);
            }
            catch(Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> Update(int id, Setting Entity)
        {
            try
            {
                var settingAccount = await _db.SettingsAccounts.Include(x => x.Setting).AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Setting_Id == id && x.Account_Id == Entity.SettingAccounts.First().Account_Id);
                if (settingAccount == null)
                {
                    var errors = new List<string>()
                    {
                        new string($"Account does not have '{Entity.Key}' setting.")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }
                var newSettingAccount = new SettingAccount
                {
                    Account_Id = Entity.SettingAccounts.First().Account_Id,
                    Setting_Id = Entity.Id,
                    IsActive = Entity.SettingAccounts.First().IsActive,
                };

                Entity.SettingAccounts.Clear();
                Entity.SettingAccounts.Add(newSettingAccount);

                _db.SettingsAccounts.Update(newSettingAccount);
                return new BaseReponse<Setting>(Entity, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }

        public async Task<IBaseResponse<Setting>> UpdateGlobalSetting(int id, Setting model)
        {
            try
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(x => x.Key == model.Key);

                if(setting == null)
                {
                    var errors = new List<string>()
                    {
                        new string("Key of setting cannot be changed")
                    };
                    return new BaseReponse<Setting>(null, errors);
                }

                var newSetting = new Setting
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                };

                _db.Settings.Update(newSetting);
                return new BaseReponse<Setting>(newSetting, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Setting>(null, errors);
            }
        }
    }
}
