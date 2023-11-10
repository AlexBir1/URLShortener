using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;

namespace URLShortener.DataAccessLayer.Interfaces
{
    public interface IAboutContentRepository
    {
        public Task<IBaseResponse<AboutContent>> GetContent();
        public Task<IBaseResponse<AboutContent>> UpdateContent(AboutContent entity);
    }
}
