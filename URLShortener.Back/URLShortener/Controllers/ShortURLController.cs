using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.Models;
using URLShortener.Shortener;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortURLController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ShortURLController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<IEnumerable<ShortURLModel>>>> GetUrls()
        {
            try
            {
                var result = await _uow.ShortURLs.GetAll();
                if(result.Data is not null)
                {
                    var models = new List<ShortURLModel>();
                    foreach (var item in result.Data)
                    {
                        models.Add(new ShortURLModel 
                        {
                            Id = item.Id,
                            Url = item.Url,
                            Origin = item.Origin,
                        });
                    }
                    return new BaseReponse<IEnumerable<ShortURLModel>>(models, result.Errors);
                }
                return new BaseReponse<IEnumerable<ShortURLModel>>(null, result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IBaseResponse<ShortURLInfoModel>>> GetUrl(int id)
        {
            try
            {
                var result = await _uow.ShortURLs.GetById(id);
                if (result.Data is not null)
                {
                    var model = new ShortURLInfoModel()
                    {
                       Id = result.Data.Id,
                       Url = result.Data.Url,
                       Origin = result.Data.Origin,
                       CreatedBy = result.Data.Info.CreatedBy,
                       CreationDate = result.Data.Info.CreationDate,
                    };
                    return new BaseReponse<ShortURLInfoModel>(model, result.Errors);
                }
                return new BaseReponse<ShortURLInfoModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("TryRedirect/{pathname}")]
        public async Task<ActionResult<IBaseResponse<string>>> TryRedirect(string pathname)
        {
            try
            {
                var urls = await _uow.ShortURLs.GetAll();
                var url = urls.Data.FirstOrDefault(x=>x.Url.Contains(pathname));
                if(url == null)
                {
                    return new BaseReponse<string>(null, null);
                }

                var originURL = await _uow.ShortURLs.GetById(url.Id);

                if(originURL != null)
                    return new BaseReponse<string>(originURL.Data.Origin, null);
                return new BaseReponse<string>(null, null);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<IBaseResponse<ShortURLModel>>> AddUrl(ShortURLModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<ShortURLModel>(null, modelErrors);
                }

                var newUrl = new ShortURL
                {
                    Url = model.Url.ShortenURL(model.CreatedBy),
                    Origin = model.Origin,
                    Info = new ShortURLInfo
                    {
                        CreatedBy = model.CreatedBy,
                        CreationDate = DateTime.Today
                    }
                };

                var result = await _uow.ShortURLs.Insert(newUrl);

                if (result.Data is not null)
                {
                    var urlModel = new ShortURLModel
                    {
                        Id = result.Data.Id,
                        Url = result.Data.Url,
                        Origin = result.Data.Origin,
                    };
                    return new BaseReponse<ShortURLModel>(urlModel, null);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<IBaseResponse<ShortURLModel>>> DeleteUrl(int id)
        {
            try
            {
                var result = await _uow.ShortURLs.Delete(id);
                if (result.Data is not null)
                {
                    await _uow.CommitAsync();
                    var model = new ShortURLModel 
                    {
                        Id = id,
                        Url = result.Data.Url
                    };
                    return new BaseReponse<ShortURLModel>(model, result.Errors);
                }
                return new BaseReponse<ShortURLModel>(null, result.Errors);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
