using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.Models;

namespace URLShortener.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<IBaseResponse<AccountModel>> DeleteAccount(string id);
        public Task<IBaseResponse<AccountModel>> UpdateAccount(string id, UpdateAccountModel model);
        public Task<IBaseResponse<AccountModel>> GetAccountById(string id);

        public Task<IBaseResponse<AccountModel>> RefreshAccountJWT(string accountId);
        public Task<IBaseResponse<AccountModel>> GetAccountByUsername(string username);
        public Task<IBaseResponse<AccountModel>> SignUp(SignUpModel model);
        public Task<IBaseResponse<AccountModel>> SignIn(SignInModel model);
    }
}
