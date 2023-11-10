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
    public interface IShortURLService
    {
        public Task<IBaseResponse<ShortURLModel>> GetShortURLByShortenURLPathname(string pathname);
        public Task<IBaseResponse<ShortURLModel>> InsertShortURL(ShortURLModel Entity);
        public Task<IBaseResponse<ShortURLModel>> DeleteShortURL(int id);
        public Task<IBaseResponse<ShortURLModel>> UpdateShortURL(int id, ShortURLModel Entity);
        public Task<IBaseResponse<IEnumerable<ShortURLModel>>> GetAllShortURLs();
        public Task<IBaseResponse<ShortURLInfoModel>> GetShortURLById(int id);
    }
}
