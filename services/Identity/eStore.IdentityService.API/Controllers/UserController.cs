using eStore.IdentityService.API.Models;
using eStore.IdentityService.API.Services;
using eStore.IdentityService.Core.Entity;
using eStore.IdentityService.Core.Extensions;
using eStore.Shared.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;

namespace eStore.IdentityService.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserServices _userServices;
        private readonly IBasicAuthenticationServices _basicAuthenticationServices;
        private readonly IOptions<RuntimeSettings> _runtimeSettings;
        private readonly ITokenAuthenticationServices _tokenAuthenticationServices;
        public UserController(ILogger<UserController> logger,
            IUserServices userServices,
            IOptions<RuntimeSettings> runtimeSettings,
            IBasicAuthenticationServices basicAuthenticationServices,
            ITokenAuthenticationServices tokenAuthenticationServices)
        {
            _logger = logger;
            this._userServices = userServices;
            this._runtimeSettings = runtimeSettings;
            Utilities.RuntimeSettings = runtimeSettings.Value;
            this._basicAuthenticationServices = basicAuthenticationServices;
            _tokenAuthenticationServices = tokenAuthenticationServices;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            model.Password = Utilities.Encrpyt(model.Password);
            var user = await _basicAuthenticationServices.Authenticate(model.Username, model.Password);

            if (user == null)
                return base.StatusCode<AuthenticateModel>(HttpStatusCode.Unauthorized, model, false);

            return base.StatusCode<AuthenticateModel>(HttpStatusCode.OK, user, true);
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken(TokenEntity tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = tokenModel.Token;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = _tokenAuthenticationServices.CheckTokenExpire(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            long userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var identity = await this._userServices.GetUserByIdAsync(userId);

            return new ObjectResult(new
            {
                //accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                //refreshToken = newRefreshToken
            });
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] AuthorizationUser model)
        {
            User response = new User();
            TokenModel currentToken = new TokenModel(); ;

            try
            {
                response = await this._userServices.GetUserAsync(model.UserName, model.Password);

                if (response != null)
                {
                    var tokenEntity = await _tokenAuthenticationServices.Authenticate(response);
                    currentToken = currentToken.ConvertTokenModel(tokenEntity);
                }
                else
                {
                    return base.ErrorCode<TokenModel>(HttpStatusCode.OK, currentToken, null, "Kullanıcı Bulunamadı");
                }

            }
            catch (Exception ex)
            {
                return base.ErrorCode<TokenModel>(HttpStatusCode.OK, currentToken, ex);
            }

            return base.StatusCode<TokenModel>(HttpStatusCode.OK, currentToken, true);
        }

        [HttpPost]
        public async Task<IActionResult> GetUserByTokenAsync()
        {
            UserModel response = new UserModel();
            long userId = Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                var identity = await this._userServices.GetUserByIdAsync(userId);
                if (identity != null)
                {
                    response = new UserModel()
                    {
                        Id = identity.Id,
                        Firstname = identity.Firstname,
                        Lastname = identity.Lastname,
                        UserType = identity.UserType,
                        EMail = identity.EMail,
                        PhoneNumber = identity.PhoneNumber
                    };
                }
                else
                {
                    return base.ErrorCode<UserModel>(HttpStatusCode.OK, response, null, "Kullanıcı Bulunamadı");
                }
            }
            catch (Exception ex)
            {
                return base.ErrorCode<UserModel>(HttpStatusCode.OK, response, ex);

            }

            return base.StatusCode(HttpStatusCode.OK, response, true);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserModel userInfo)
        {
            try
            {
                var encryptionIV = this._runtimeSettings.Value.EncryptionIV;
                var encryptionKey = this._runtimeSettings.Value.EncryptionKey;

                User userData = new User(userInfo.Firstname, userInfo.Lastname, userInfo.EMail, userInfo.PhoneNumber, Utilities.Encrpyt(userInfo.Password), userInfo.UserType);
                await this._userServices.CreateUserAsync(userData);
            }
            catch (Exception ex)
            {
                return this.Problem(ex.StackTrace, null, 500, ex.Message);
            }

            return base.StatusCode(200);

        }
    }
}