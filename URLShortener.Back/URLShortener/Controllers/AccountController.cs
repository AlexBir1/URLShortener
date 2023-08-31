using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using URLShortener.DataAccessLayer.BaseResponse;
using URLShortener.DataAccessLayer.Entities;
using URLShortener.DataAccessLayer.Interfaces;
using URLShortener.DataAccessLayer.JWT;
using URLShortener.DataAccessLayer.Repositories;
using URLShortener.Models;

namespace URLShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public AccountController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [Authorize]
        [HttpGet("refreshToken")]
        public async Task<ActionResult<AccountModel>> RefreshToken()
        {
            var result = await _uow.Accounts.RefreshJWT(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (result.Data is not null)
                return result.Data;
            else
                return BadRequest();
            
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult<BaseReponse<AccountModel>>> SignUp(SignUpModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var modelErrors = ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<AccountModel>(null, modelErrors);
                }

                var result = await _uow.Accounts.SignUp(model);
                if (result.Data is null)
                {
                    return new BaseReponse<AccountModel>(null, result.Errors.ToArray());
                }
                return new BaseReponse<AccountModel>(result.Data, null);

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
                    var modelErrors = ModelState.Where(x => x.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();
                    return new BaseReponse<AccountModel>(null, modelErrors);
                }

                var result = await _uow.Accounts.SignIn(model);
                if(result.Data is null)
                {
                    return new BaseReponse<AccountModel>(null, result.Errors.ToArray());
                }
                return new BaseReponse<AccountModel>(result.Data, null);

            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
