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
    public interface IAboutContentService
    {
        public Task<IBaseResponse<AboutContentModel>> GetContent();
        public Task<IBaseResponse<AboutContentModel>> UpdateContent(AboutContentModel model);
    }
}
