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
    public interface IAccountRepository : IUserRepository<Account>
    {
        public Task<IBaseResponse<AccountModel>> RefreshJWT(string accountId);
        public Task<IBaseResponse<Account>> GetByUsername(string username);
        public Task<IBaseResponse<AccountModel>> SignUp(Account entity, string password);
        public Task<IBaseResponse<AccountModel>> SignIn(Account entity, string password);
    }
}
