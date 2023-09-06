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

        public async Task<IBaseResponse<SettingModel>> Delete(int id)
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
                    return new BaseReponse<SettingModel>(null, errors);
                }
                _db.Settings.Remove(setting);

                var settingModel = new SettingModel
                {
                    Id = setting.Id,
                    Key = setting.Key,
                    Title = setting.Title,
                    Description = setting.Description,
                };

                return new BaseReponse<SettingModel>(settingModel, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> GetAccountSetting(int settingId, string accountId)
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
                    return new BaseReponse<SettingModel>(null, errors);
                }

                var settingModel = new SettingModel
                {
                    Id = setting.Setting_Id,
                    Key = setting.Setting.Key,
                    Title = setting.Setting.Title,
                    Description = setting.Setting.Description,
                    Account_Id = setting.Account_Id,
                    IsActive = setting.IsActive,
                };

                return new BaseReponse<SettingModel>(settingModel, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> GetAll()
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
                    return new BaseReponse<IEnumerable<SettingModel>>(null, errors);
                }

                var settingsModels = settings.Select(x => new SettingModel
                {
                    Id = x.Id,
                    Key = x.Key,
                    Title = x.Title,
                    Description = x.Description,
                }).ToList();

                return new BaseReponse<IEnumerable<SettingModel>>(settingsModels, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<SettingModel>>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllByAccountId(string accountId)
        {
            try
            {
                var settingsAccounts = await _db.SettingsAccounts.Include(x => x.Setting)
                    .Where(x => x.Account_Id == accountId).ToListAsync();
                if (settingsAccounts == null)
                {
                    var errors = new List<string>()
                    {
                        new string("No settings were found")
                    };
                    return new BaseReponse<IEnumerable<SettingModel>>(null, errors);
                }

                var settingsAccountModels = settingsAccounts.Select(x => new SettingModel
                {
                    Id = x.Setting.Id,
                    Account_Id = x.Account_Id,
                    Key = x.Setting.Key,
                    Title = x.Setting.Title,
                    Description = x.Setting.Description,
                    IsActive = x.IsActive,
                }).ToList();

                return new BaseReponse<IEnumerable<SettingModel>>(settingsAccountModels, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<SettingModel>>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> GetById(int id)
        {
            try
            {
                var settingAccount = await _db.Settings
                    .FirstOrDefaultAsync(x => x.Id == id);
                if (settingAccount == null)
                {
                    var errors = new List<string>()
                    {
                        new string("This setting does not exist")
                    };
                    return new BaseReponse<SettingModel>(null, errors);
                }

                var settingAccountModel = new SettingModel
                {
                    Id = id,
                    Title = settingAccount.Title,
                    Description = settingAccount.Description,
                    Key = settingAccount.Key,
                };

                return new BaseReponse<SettingModel>(settingAccountModel, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> Insert(SettingModel Entity)
        {
            try
            {
                var settingAccount = new SettingAccount
                {
                    Account_Id = Entity.Account_Id,
                    Setting_Id = Entity.Id,
                    IsActive = Entity.IsActive,
                };

                await _db.SettingsAccounts.AddAsync(settingAccount);

                return new BaseReponse<SettingModel>(Entity, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> InsertNewSetting(SettingModel model)
        {
            try
            {
                var setting = await _db.Settings.FirstOrDefaultAsync(x => x.Key == model.Key);
                if(setting == null)
                {
                    var errors = new List<string>()
                    {
                        new string("This setting exists")
                    };
                    return new BaseReponse<SettingModel>(null, errors);
                }
                var newSetting = new Setting
                {
                    Id = 0,
                    Key = model.Key,
                    Title = model.Title,
                    Description = model.Description,
                };

                _db.Settings.Add(newSetting);

                model.Id = newSetting.Id;

                return new BaseReponse<SettingModel>(model, null);
            }
            catch(Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> Update(int id, SettingModel Entity)
        {
            try
            {
                var settingAccount = await _db.SettingsAccounts.Include(x => x.Setting).AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Setting_Id == id && x.Account_Id == Entity.Account_Id);
                if (settingAccount == null)
                {
                    var errors = new List<string>()
                    {
                        new string($"Account does not have '{Entity.Key}' setting.")
                    };
                    return new BaseReponse<SettingModel>(null, errors);
                }
                var newSettingAccount = new SettingAccount
                {
                    Account_Id = Entity.Account_Id,
                    Setting_Id = Entity.Id,
                    IsActive = Entity.IsActive,
                };

                _db.SettingsAccounts.Update(newSettingAccount);
                return new BaseReponse<SettingModel>(Entity, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<SettingModel>> UpdateSetting(int id, SettingModel model)
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
                    return new BaseReponse<SettingModel>(null, errors);
                }

                var newSetting = new Setting
                {
                    Id = id,
                    Title = model.Title,
                    Description = model.Description,
                };

                _db.Settings.Update(newSetting);
                return new BaseReponse<SettingModel>(model, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<SettingModel>(null, errors);
            }
        }
    }
}
