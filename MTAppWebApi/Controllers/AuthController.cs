using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MTApp.Utilities.JWTAuthentication;
using MTAPP.DAL.Model;
using MTAPP.DAL.Repository;
using MTAPP.Model;
using MTAppWebApi.Controllers.Utilities.Extension;
using MTAppWebApi.Service;
using MTAppWebApi.Utilities;
using System.Text;

namespace MTAppWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IGenericRepository<AppUser> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTTokenHelper _jWTTokenHelper;
        private readonly IAuthService _authService;

        public AuthController(IGenericRepository<AppUser> userRepository, IHttpContextAccessor httpContextAccessor,
            IJWTTokenHelper jWTTokenHelper, IAuthService authService)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
            _jWTTokenHelper = jWTTokenHelper;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult OAuthToken()
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.GetHeader("Authorization");
            if (authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));
                int seperatorIndex = usernamePassword.IndexOf(':');
                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);
                var userdetails = _authService.ValidateUser(username, password);
                if(userdetails != null)
                {
                    var claims = new Dictionary<string, string>();
                    claims.Add("userid", userdetails.userid.ToString());
                    claims.Add("roleid", userdetails.roleid.ToString());
                    claims.Add("username", userdetails.username);
                    var token = _jWTTokenHelper.GenerateJWTToken(claims);
                    userdetails.refreshtoken = token.RefreshToken;
                    _userRepository.Update(UserServiceUtility.ConvertToDBModel(userdetails));
                    return Ok(token);
                }
                else
                    return Unauthorized("Invalid credential");
            }
            else
            {
                //Handle what happens if that isn't the case
                //throw new Exception("The authorization header is either empty or isn't Basic.");
                return StatusCode(500, "The authorization header is either empty or isn't Basic.");
            }
        }
    }
}

