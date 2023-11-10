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
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _uowRepo;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<AccountModel>> DeleteAccount(string id)
        {
            try
            {
                var result = await _uowRepo.Accounts.Delete(id);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(_mapper.Map<AccountModel>(result.Data), null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> GetAccountById(string id)
        {
            try
            {
                var result = await _uowRepo.Accounts.GetById(id);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(_mapper.Map<AccountModel>(result.Data), null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> GetAccountByUsername(string username)
        {
            try
            {
                var result = await _uowRepo.Accounts.GetByUsername(username);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(_mapper.Map<AccountModel>(result.Data), null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> RefreshAccountJWT(string accountId)
        {
            try
            {
                var result = await _uowRepo.Accounts.RefreshJWT(accountId);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(result.Data, null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> SignIn(SignInModel model)
        {
            try
            {
                var result = await _uowRepo.Accounts.SignIn(_mapper.Map<Account>(model), model.Password);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(result.Data, null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> SignUp(SignUpModel model)
        {
            try
            {
                var result = await _uowRepo.Accounts.SignUp(_mapper.Map<Account>(model), model.PasswordConfirm);
                if (result.Data != null)
                {
                    return new BaseReponse<AccountModel>(result.Data, null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> UpdateAccount(string id, UpdateAccountModel model)
        {
            try
            {
                var result = await _uowRepo.Accounts.Update(id, _mapper.Map<Account>(model), _mapper.Map<ChangePasswordProperties>(model));
                if(result.Data != null)
                {
                    return new BaseReponse<AccountModel>(_mapper.Map<AccountModel>(result.Data), null);
                }
                return new BaseReponse<AccountModel>(null, result.Errors);
            }
            catch (Exception ex) 
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AccountModel>(null, errors);
            }
        }
    }
}
