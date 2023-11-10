using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _service;

        public SettingController(ISettingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<IEnumerable<SettingModel>>>> GetAllSettings()
        {
            try
            {
                return Ok(await _service.GetAllGlobalSettings());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{accountId}")]
        public async Task<ActionResult<IBaseResponse<IEnumerable<SettingModel>>>> GetAccountSettings(string accountId)
        {
            try
            {
                return Ok(await _service.GetAllUserSettings(accountId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{settingId}/{accountId}")]
        public async Task<ActionResult<IBaseResponse<SettingModel>>> GetAccountSetting(int settingId, string accountId)
        {
            try
            {
                return Ok(await _service.GetUserSettingById(accountId, settingId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("{accountId}")]
        public async Task<ActionResult<IBaseResponse<IEnumerable<SettingModel>>>> AddAccountSettings(string accountId, IEnumerable<SettingModel> settings)
        {
            try
            {
                return Ok(await _service.InsertUserSettings(accountId, settings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("{accountId}")]
        public async Task<ActionResult<IBaseResponse<IEnumerable<SettingModel>>>> UpdateAccountSettings(string accountId, IEnumerable<SettingModel> settings)
        {
            try
            {
                return Ok(await _service.UpdateUserSettings(accountId, settings));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }           
        }
    }
}
