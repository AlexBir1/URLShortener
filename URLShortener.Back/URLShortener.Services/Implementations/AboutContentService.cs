using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Services.Implementations
{
    public class AboutContentService : IAboutContentService
    {
        private readonly IUnitOfWork _uowRepo;
        private readonly IMapper _mapper;

        public AboutContentService(IUnitOfWork uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<AboutContentModel>> GetContent()
        {
            try
            {
                var result = await _uowRepo.AboutPageContent.GetContent();
                if (result.Data is not null)
                {
                    return new BaseReponse<AboutContentModel>(_mapper.Map<AboutContentModel>(result.Data), null);
                }
                else
                    return new BaseReponse<AboutContentModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AboutContentModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<AboutContentModel>> UpdateContent(AboutContentModel model)
        {
            try
            {
                var result = await _uowRepo.AboutPageContent.UpdateContent(_mapper.Map<AboutContent>(model));
                if(result.Data is not null)
                {
                    return new BaseReponse<AboutContentModel>(_mapper.Map<AboutContentModel>(result.Data), null);
                }
                else
                    return new BaseReponse<AboutContentModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<AboutContentModel>(null, errors);
            }
        }
    }
}
