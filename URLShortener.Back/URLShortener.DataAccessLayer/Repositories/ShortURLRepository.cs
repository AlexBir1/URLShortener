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

namespace URLShortener.DataAccessLayer.Repositories
{
    public class ShortURLRepository : IShortURLRepository
    {
        private readonly AppDBContext _db;

        public ShortURLRepository(AppDBContext db)
        {
            _db = db;
        }
        public async Task<IBaseResponse<ShortURL>> Delete(int id)
        {
            try
            {
                var data = await _db.ShortURLs.Include(x=>x.Info).FirstAsync(url => url.Id == id);
                _db.ShortURLs.Remove(data);
                return new BaseReponse<ShortURL>(data, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURL>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<ShortURL>>> GetAll()
        {
            try
            {
                var data = await _db.ShortURLs.Include(x=>x.Info).ToListAsync();
                return new BaseReponse<IEnumerable<ShortURL>>(data, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<ShortURL>>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURL>> GetById(int id)
        {
            try
            {
                var data = await _db.ShortURLs.Include(x => x.Info)
                    .FirstAsync(url => url.Id == id);

                return new BaseReponse<ShortURL>(data, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURL>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURL>> GetByShortenURLPathname(string pathname)
        {
            try
            {
                var url = await _db.ShortURLs
                    .FirstAsync(x => x.Url.Contains(pathname));

                return new BaseReponse<ShortURL>(url, null);
            }
            catch(Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURL>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURL>> Insert(ShortURL Entity)
        {
            try
            {
                var urlFromDB = await _db.ShortURLs.FirstOrDefaultAsync(x => x.Origin == Entity.Origin);
                if (urlFromDB != null)
                {
                    var errors = new List<string>
                    {
                        new string("This link was shortened before.")
                    };
                    return new BaseReponse<ShortURL>(null, errors);
                }

                var url = _db.ShortURLs.Add(Entity);

                return new BaseReponse<ShortURL>(url.Entity, null);

            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURL>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURL>> Update(int id, ShortURL Entity)
        {
            throw new NotImplementedException();
        }
    }
}
