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
    public interface IUserRepository<T>
    {
        public Task<IBaseResponse<T>> Insert(T Entity, string password, string role);
        public Task<IBaseResponse<T>> Delete(string id);
        public Task<IBaseResponse<T>> Update(string id, T Entity, ChangePasswordProperties passProps);
        public Task<IBaseResponse<IEnumerable<T>>> GetAll();
        public Task<IBaseResponse<T>> GetById(string id);
    }
}
