using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutContentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AboutContentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<AboutContent>>> GetContent()
        {
            try
            {
                var result = await _unitOfWork._db.AboutContent.FirstOrDefaultAsync(x => x.Id == 1);
                if (result != null)
                    return new BaseReponse<AboutContent>(result, null);
                else
                    return new BaseReponse<AboutContent>(null, null);
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
                _unitOfWork._db.AboutContent.Update(model);
                await _unitOfWork.CommitAsync();
                return new BaseReponse<AboutContent>(model, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
