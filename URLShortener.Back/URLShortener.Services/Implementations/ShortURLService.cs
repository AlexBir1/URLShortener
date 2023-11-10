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
using URLShortener.Shortener;

namespace URLShortener.Services.Implementations
{
    public class ShortURLService : IShortURLService
    {
        private readonly IUnitOfWork _uowRepo;
        private readonly IMapper _mapper;

        public ShortURLService(IUnitOfWork uowRepo, IMapper mapper)
        {
            _uowRepo = uowRepo;
            _mapper = mapper;
        }

        public async Task<IBaseResponse<ShortURLModel>> DeleteShortURL(int id)
        {
            try
            {
                var result = await _uowRepo.ShortURLs.Delete(id);
                await _uowRepo.CommitAsync();
                if (result.Data != null)
                {
                    return new BaseReponse<ShortURLModel>(_mapper.Map<ShortURLModel>(result.Data), null);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURLModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<IEnumerable<ShortURLModel>>> GetAllShortURLs()
        {
            try
            {
                var result = await _uowRepo.ShortURLs.GetAll();
                if (result.Data != null)
                {
                    return new BaseReponse<IEnumerable<ShortURLModel>>(_mapper.Map<IEnumerable<ShortURLModel>>(result.Data), null);
                }
                return new BaseReponse<IEnumerable<ShortURLModel>>(null, result.Errors);

            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<IEnumerable<ShortURLModel>>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURLInfoModel>> GetShortURLById(int id)
        {
            try
            {
                var result = await _uowRepo.ShortURLs.GetById(id);
                if (result.Data != null)
                {
                    return new BaseReponse<ShortURLInfoModel>(_mapper.Map<ShortURLInfoModel>(result.Data), null);
                }
                return new BaseReponse<ShortURLInfoModel>(null, result.Errors);

            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURLInfoModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURLModel>> GetShortURLByShortenURLPathname(string pathname)
        {
            try
            {
                var result = await _uowRepo.ShortURLs.GetByShortenURLPathname(pathname);
                if (result.Data != null)
                {
                    return new BaseReponse<ShortURLModel>(_mapper.Map<ShortURLModel>(result.Data), null);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURLModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURLModel>> InsertShortURL(ShortURLModel Entity)
        {
            try
            {
                var newShortURL = _mapper.Map<ShortURL>(Entity);
                newShortURL.Url = ShortURLMaker.ShortenURL(newShortURL.Origin, Entity.CreatedBy);
                var result = await _uowRepo.ShortURLs.Insert(newShortURL);
                await _uowRepo.CommitAsync(); 
                if (result.Data != null)
                {
                    return new BaseReponse<ShortURLModel>(_mapper.Map<ShortURLModel>(result.Data), null);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURLModel>(null, errors);
            }
        }

        public async Task<IBaseResponse<ShortURLModel>> UpdateShortURL(int id, ShortURLModel Entity)
        {
            try
            {
                var result = await _uowRepo.ShortURLs.Update(id, _mapper.Map<ShortURL>(Entity));
                await _uowRepo.CommitAsync();
                if (result.Data != null)
                {
                    return new BaseReponse<ShortURLModel>(_mapper.Map<ShortURLModel>(result.Data), null);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                var errors = new List<string>
                {
                    ex.Message
                };
                if (!string.IsNullOrEmpty(ex.InnerException?.Message))
                    errors.Add(ex.InnerException.Message);
                return new BaseReponse<ShortURLModel>(null, errors);
            }
        }
    }
}
