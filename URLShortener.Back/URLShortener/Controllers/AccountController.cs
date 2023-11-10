using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.DataAccessLayer.Repositories;
using URLShortener.DataAccessLayer.UOW;
using URLShortener.Models;
using URLShortener.Services.Interfaces;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("refreshToken")]
        public async Task<ActionResult<AccountModel>> RefreshToken()
        {
            try
            {
                var result = await _service.RefreshAccountJWT(User.FindFirstValue(ClaimTypes.NameIdentifier));

                if(result.Data is null)
                {
                    return BadRequest(result.Errors);
                }

                return result.Data;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<BaseReponse<AccountModel>>> SignUp([FromBody] SignUpModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<AccountModel>(null, modelErrors);
                }

                return Ok(await _service.SignUp(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult<BaseReponse<AccountModel>>> SignIn(SignInModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .SelectMany(x => x.Value.Errors)
                        .Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<AccountModel>(null, modelErrors);
                }

                return Ok(await _service.SignIn(model));

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<BaseReponse<AccountModel>>> UpdateAccount(string id, UpdateAccountModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<AccountModel>(null, modelErrors);
                }

                var updateResult = await _service.UpdateAccount(id, model);
                if(updateResult.Data is null)
                {
                    return new BaseReponse<AccountModel>(null, updateResult.Errors.ToArray());
                }

                var refreshTokenResult = await _service.RefreshAccountJWT(id);
                if (refreshTokenResult.Data is null)
                {
                    return new BaseReponse<AccountModel>(null, refreshTokenResult.Errors.ToArray());
                }

                return Ok(refreshTokenResult);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
