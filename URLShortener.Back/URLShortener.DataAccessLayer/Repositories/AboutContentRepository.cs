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
using URLShortener.DataAccessLayer.UOW;

namespace URLShortener.DataAccessLayer.Repositories
{
    public class AboutContentRepository : IAboutContentRepository
    {
        private readonly AppDBContext _db;

        public AboutContentRepository(AppDBContext db)
        {
            _db = db;
        }

        public async Task<IBaseResponse<AboutContent>> GetContent()
        {
            try
            {
                var result = await _db.AboutContent.SingleAsync(x => x.Id == 1);
                return new BaseReponse<AboutContent>(result, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AboutContent>(null, errors);
            }
        }

        public async Task<IBaseResponse<AboutContent>> UpdateContent(AboutContent entity)
        {
            try
            {
                _db.AboutContent.Update(entity);
                return new BaseReponse<AboutContent>(entity, null);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AboutContent>(null, errors);
            }
        }
    }
}
