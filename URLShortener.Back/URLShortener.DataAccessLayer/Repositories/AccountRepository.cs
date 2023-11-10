using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.Models;

namespace URLShortener.DataAccessLayer.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _config;
        private readonly JWTService _jwtService;
        private readonly SignInManager<Account> _signInManager;

        public AccountRepository(UserManager<Account> userManager, IConfiguration cofig, JWTService jwtService, SignInManager<Account> signInManager)
        {
            _userManager = userManager;
            _config = cofig;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }

        public async Task<IBaseResponse<Account>> Delete(string id)
        {
            try
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account == null)
                {
                    var result = await _userManager.DeleteAsync(account);
                    if (result.Succeeded)
                    {
                        return new BaseReponse<Account>(account, null);
                    }
                    return new BaseReponse<Account>(null, result.Errors.Select(x => x.Description).ToArray());
                }
                return new BaseReponse<Account>(null, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Account>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<Account>>> GetAll()
        {
            try
            {
                return new BaseReponse<IEnumerable<Account>>(null, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<Account>>(null, errors);
            }
        }

        public async Task<IBaseResponse<Account>> GetById(string id)
        {
            try
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account == null)
                {
                    var errors = new List<string>()
                    {
                        new string("Account does not exist.")
                    };
                    return new BaseReponse<Account>(null, errors);
                }
                return new BaseReponse<Account>(account, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Account>(null, errors);
            }
        }

        public async Task<IBaseResponse<Account>> GetByUsername(string username)
        {
            try
            {
                var account = await _userManager.FindByNameAsync(username);
                if (account == null)
                {
                    var errors = new List<string>()
                    {
                        new string("Account does not exist.")
                    };
                    return new BaseReponse<Account>(null, errors);
                }
                return new BaseReponse<Account>(account, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Account>(null, errors);
            }
        }

        public async Task<IBaseResponse<Account>> Insert(Account Entity, string password, string role)
        {
            try
            {
                var account = await _userManager.FindByNameAsync(Entity.UserName);
                if (account != null)
                {
                    var errors = new List<string>()
                    {
                        new string("Account already exists.")
                    };
                    return new BaseReponse<Account>(null, errors);
                }
                else
                {
                    var result = await _userManager.CreateAsync(Entity, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(Entity, role);
                        return new BaseReponse<Account>(Entity, null);
                    }
                    return new BaseReponse<Account>(null, result.Errors.Select(x => x.Description).ToArray());
                }

            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Account>(null, errors);
            }
        }

        public async Task<IBaseResponse<AccountModel>> RefreshJWT(string accountId)
        {
            var Account = await _userManager.FindByIdAsync(accountId);
            var rolesList = await _userManager.GetRolesAsync(Account);
            var token = await _jwtService.CreateToken(new JWTTokenProperty(_config["JWT:Key"], int.Parse(_config["JWT:ExpiresInDays"]), Account));

            var accountWithJWT = new AccountModel()
            {
                Id = Account.Id,
                JWTToken = token,
                Username = Account.UserName,
                Role = rolesList[0],
            };

            return new BaseReponse<AccountModel>(accountWithJWT, null);

        }

        public async Task<IBaseResponse<AccountModel>> SignIn(Account entity, string password)
        {
            try
            {
                var account = await _userManager.FindByNameAsync(entity.UserName);
                if (account != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(account, password, false);
                    if (result.Succeeded)
                    {
                        var rolesList = await _userManager.GetRolesAsync(account);
                        var token = await _jwtService.CreateToken(new JWTTokenProperty(_config["JWT:Key"], int.Parse(_config["JWT:ExpiresInDays"]), account));
                        var accountWithJWT = new AccountModel()
                        {
                            Id = account.Id,
                            JWTToken = token,
                            Username = account.UserName,
                            Role = rolesList[0],
                        };
                        return new BaseReponse<AccountModel>(accountWithJWT, null);
                    }
                    var errors = new List<string>
                    {
                        new string("Invalid login or password"),
                    };
                    return new BaseReponse<AccountModel>(null, errors);
                }
                else
                {
                    var errors = new List<string>
                    {
                        new string("Invalid login or password"),
                    };
                    return new BaseReponse<AccountModel>(null, errors);
                }
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

        public async Task<IBaseResponse<AccountModel>> SignUp(Account entity, string password)
        {
            try
            {
                var account = await _userManager.FindByNameAsync(entity.UserName);
                if (account == null)
                {
                    var result = await _userManager.CreateAsync(entity, password);
                    if (result.Succeeded)
                    {
                        var createdAccount = await _userManager.FindByNameAsync(entity.UserName);
                        await _userManager.AddToRoleAsync(createdAccount, "User");
                        var rolesList = await _userManager.GetRolesAsync(createdAccount);
                        var token = await _jwtService.CreateToken(new JWTTokenProperty(_config["JWT:Key"], int.Parse(_config["JWT:ExpiresInDays"]), createdAccount));
                        var accountWithJWT = new AccountModel()
                        {
                            Id = createdAccount.Id,
                            JWTToken = token,
                            Username = createdAccount.UserName,
                            Role = rolesList[0],
                        };
                        return new BaseReponse<AccountModel>(accountWithJWT, null);
                    }
                    return new BaseReponse<AccountModel>(null, result.Errors.Select(x=>x.Description).ToArray());
                }
                else
                {
                    var errors = new List<string>
                    {
                        new string("Account already exists"),
                    };
                    return new BaseReponse<AccountModel>(null, errors);
                }
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

        public async Task<IBaseResponse<Account>> Update(string id, Account Entity, ChangePasswordProperties PassProps)
        {
            try
            {
                var account = await _userManager.FindByIdAsync(id);
                if (account != null)
                {
                    if(account.UserName == _config["AdminDefaults:Username"] && account.UserName != Entity.UserName)
                    {
                        var errors = new List<string>
                        {
                            new string("The account`s username cannot be changed.")
                        };
                        return new BaseReponse<Account>(null, errors);
                    }
                    if (!string.IsNullOrEmpty(PassProps.OldPassword))
                    {
                        var resultChangedPassword = await _userManager.ChangePasswordAsync(account, PassProps.OldPassword, PassProps.ConfirmNewPassword);
                        if (account.UserName != Entity.UserName)
                        {
                            var resultChangedUsername = await _userManager.SetUserNameAsync(account, Entity.UserName);
                        }
                        if (resultChangedPassword.Succeeded)
                            return new BaseReponse<Account>(account, null);
                        return new BaseReponse<Account>(null, resultChangedPassword.Errors.Select(x=>x.Description).ToArray());
                    }
                    if (account.UserName != Entity.UserName)
                    {
                        var resultChangedUsername = await _userManager.SetUserNameAsync(account, Entity.UserName);
                        if (resultChangedUsername.Succeeded)
                            return new BaseReponse<Account>(account, null);
                    }
                }

                return new BaseReponse<Account>(null, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<Account>(null, errors);
            }
        }
    }
}
