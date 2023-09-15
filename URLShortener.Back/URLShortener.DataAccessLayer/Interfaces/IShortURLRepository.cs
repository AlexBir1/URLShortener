using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface IShortURLRepository : IRepository<ShortURL>
    {
        public Task<IBaseResponse<ShortURL>> GetOriginByShortenURLPathname(string pathname);
    }
}
