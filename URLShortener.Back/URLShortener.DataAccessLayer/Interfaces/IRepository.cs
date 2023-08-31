using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface IRepository<T>
    {
        public Task<IBaseResponse<T>> Insert(T Entity);
        public Task<IBaseResponse<T>> Delete(int id);
        public Task<IBaseResponse<T>> Update(int id, T Entity);
        public Task<IBaseResponse<IEnumerable<T>>> GetAll();
        public Task<IBaseResponse<T>> GetById(int id);
    }
}
