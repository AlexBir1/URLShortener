using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShortURLController : ControllerBase
    {
        private readonly IShortURLService _service;

        public ShortURLController(IShortURLService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<IEnumerable<ShortURLModel>>>> GetUrls()
        {
            try
            {
                return Ok(await _service.GetAllShortURLs());
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
                return Ok(await _service.GetShortURLById(id));
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
                return Ok(await _service.GetShortURLByShortenURLPathname(pathname));
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
                return Ok(await _service.InsertShortURL(model));
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
                return Ok(await _service.DeleteShortURL(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
