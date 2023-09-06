using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public SettingController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<IBaseResponse<IEnumerable<SettingModel>>>> GetAllSettings()
        {
            try
            {
                var settings = await _uow.Settings.GetAll();
                if (settings.Data.Count() == 0)
                {
                    return new BaseReponse<IEnumerable<SettingModel>>(new SettingModel[0], null);
                }
                return new BaseReponse<IEnumerable<SettingModel>>(settings.Data, null);
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
                var settings = await _uow.Settings.GetAllByAccountId(accountId);
                if (settings.Data.Count() == 0)
                {
                    return new BaseReponse<IEnumerable<SettingModel>>(new SettingModel[0], null);
                }
                return new BaseReponse<IEnumerable<SettingModel>>(settings.Data, null);
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
                var settings = await _uow.Settings.GetAccountSetting(settingId, accountId);
                if (settings.Data == null)
                {
                    return new BaseReponse<SettingModel>(null, settings.Errors.ToArray());
                }
                return new BaseReponse<SettingModel>(settings.Data, null);
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
                foreach (var setting in settings)
                {
                    await _uow.Settings.Insert(setting);
                }
                await _uow.CommitAsync();

                return new BaseReponse<IEnumerable<SettingModel>>(settings.ToArray(), null);
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
                foreach (var setting in settings)
                {
                    await _uow.Settings.Update(setting.Id,setting);
                }

                await _uow.CommitAsync();

                return new BaseReponse<IEnumerable<SettingModel>>(settings.ToArray(), null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }           
        }
    }
}
