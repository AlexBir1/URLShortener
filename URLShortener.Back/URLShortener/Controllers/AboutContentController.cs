using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutContentController : ControllerBase
    {
        private readonly IAboutContentService _service;
        public AboutContentController(IAboutContentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<AboutContent>>> GetContent()
        {
            try
            {
                return Ok(await _service.GetContent());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IBaseResponse<AboutContent>>> UpdateContent(AboutContent model)
        {
            try
            {
                AboutContentModel newModel = new AboutContentModel()
                {
                    Id = model.Id,
                    Content = model.Content
                };

                return Ok(await _service.UpdateContent(newModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
