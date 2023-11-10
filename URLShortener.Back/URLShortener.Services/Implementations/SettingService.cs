using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Services.Implementations
{
    public class SettingService : ISettingService
    {
        private readonly IUnitOfWork _uowRepo;
        private readonly IMapper _mapper;

        public SettingService(IUnitOfWork uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<SettingModel>> DeleteGlobalSetting(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<SettingModel>> DeleteUserSetting(int settingId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllGlobalSettings()
        {
            throw new NotImplementedException();
        }

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> GetAllUserSettings(string userId)
        {
            try
            {
                var result = await _uowRepo.Settings.GetAllByAccountId(userId);
                if (result.Data is not null)
                {
                    List<SettingModel> settingModels = new List<SettingModel>();

                    foreach (var setting in result.Data)
                    {
                        var newModel = _mapper.Map<SettingModel>(setting);

                        newModel.Account_Id = userId;
                        newModel.IsActive = setting.SettingAccounts.First().IsActive;

                        settingModels.Add(newModel);
                    }
                    
                    return new BaseReponse<IEnumerable<SettingModel>>(settingModels, null);
                }
                return new BaseReponse<IEnumerable<SettingModel>>(null, result.Errors);
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

        public async Task<IBaseResponse<SettingModel>> GetGlobalSettingById(int id)
        {
            try
            {
                var result = await _uowRepo.Settings.GetById(id);
                if (result.Data is not null)
                {
                    return new BaseReponse<SettingModel>(_mapper.Map<SettingModel>(result.Data), null);
                }
                return new BaseReponse<SettingModel>(null, result.Errors);
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

        public async Task<IBaseResponse<SettingModel>> GetUserSettingById(string userId, int settingId)
        {
            try
            {
                var result = await _uowRepo.Settings.GetAccountSetting(settingId, userId);
                if (result.Data is not null)
                {
                    var settingModel = _mapper.Map<SettingModel>(result.Data);

                    settingModel.Account_Id = userId;
                    settingModel.IsActive = result.Data.SettingAccounts.First().IsActive;

                    return new BaseReponse<SettingModel>(_mapper.Map<SettingModel>(result.Data), null);
                }
                return new BaseReponse<SettingModel>(null, result.Errors);
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

        public async Task<IBaseResponse<SettingModel>> InsertGlobalSetting(SettingModel model)
        {
            try
            {
                var result = await _uowRepo.Settings.InsertGlobalSetting(_mapper.Map<Setting>(model));
                if(result.Data is not null)
                {
                    return new BaseReponse<SettingModel>(_mapper.Map<SettingModel>(result.Data), null);
                }
                return new BaseReponse<SettingModel>(null, result.Errors);
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

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> InsertUserSettings(string accountId, IEnumerable<SettingModel> settings)
        {
            try
            {
                BaseReponse<IEnumerable<SettingModel>> baseReponse = new BaseReponse<IEnumerable<SettingModel>>(new List<SettingModel>(), null);
                foreach (var model in settings)
                {
                    var userSetting = _mapper.Map<Setting>(model);

                    userSetting.SettingAccounts = new List<SettingAccount>();

                    userSetting.SettingAccounts.Add(new SettingAccount
                    {
                        Account_Id = model.Account_Id,
                        IsActive = model.IsActive,
                    });

                    var result = await _uowRepo.Settings.Insert(userSetting);

                    if (result.Data is not null)
                    {
                        var settingModel = _mapper.Map<SettingModel>(result.Data);

                        settingModel.Account_Id = model.Account_Id;
                        settingModel.IsActive = result.Data.SettingAccounts.First().IsActive;

                        baseReponse.Data.Append(settingModel);
                    }
                    else
                    {
                        if (baseReponse.Errors == null)
                            baseReponse.Errors = new List<string>();

                        foreach (var error in baseReponse.Errors)
                        {
                            baseReponse.Errors.Append($"Setting Id '{model.Id}' for Account Id '{accountId}' ERROR: {error}");
                        }

                    }
                }
                await _uowRepo.CommitAsync();
                return baseReponse;

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

        public async Task<IBaseResponse<SettingModel>> UpdateGlobalSetting(int id, SettingModel model)
        {
            try
            {
                var result = await _uowRepo.Settings.UpdateGlobalSetting(id, _mapper.Map<Setting>(model));
                if (result.Data is not null)
                {
                    await _uowRepo.CommitAsync();

                    var settingModel = _mapper.Map<SettingModel>(result.Data);

                    return new BaseReponse<SettingModel>(settingModel, null);
                }
                else
                    return new BaseReponse<SettingModel>(null, result.Errors);
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

        public async Task<IBaseResponse<IEnumerable<SettingModel>>> UpdateUserSettings(string accountId,IEnumerable<SettingModel> settings)
        {
            try
            {
                BaseReponse<IEnumerable<SettingModel>> baseReponse = new BaseReponse<IEnumerable<SettingModel>>(new List<SettingModel>(), null);
                foreach (var model in settings)
                {
                    var userSetting = _mapper.Map<Setting>(model);

                    userSetting.SettingAccounts = new List<SettingAccount>();

                    userSetting.SettingAccounts.Add(new SettingAccount
                    {
                        Account_Id = model.Account_Id,
                        Setting_Id = model.Id,
                        IsActive = model.IsActive,
                    });

                    var result = await _uowRepo.Settings.Update(model.Id, userSetting);

                    if (result.Data is not null)
                    {
                        var settingModel = _mapper.Map<SettingModel>(result.Data);

                        settingModel.Account_Id = model.Account_Id;
                        settingModel.IsActive = result.Data.SettingAccounts.First().IsActive;

                        baseReponse.Data.Append(settingModel);
                    }
                    else
                    {
                        if(baseReponse.Errors == null)
                            baseReponse.Errors = new List<string>();

                        foreach (var error in baseReponse.Errors)
                        {
                            baseReponse.Errors.Append($"Setting Id '{model.Id}' for Account Id '{accountId}' ERROR: {error}");
                        }
                        
                    }
                }
                await _uowRepo.CommitAsync();
                return baseReponse;
            }
            catch(Exception ex)
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
    }
}
