using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NB.MOBILEPO.BAL.Helpers;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.BAL.Services;
using NB.MOBILEPO.WEBAPI.Extensions;
using NB.UMS.BAL.Helpers;

namespace NB.MOBILEPO.WEBAPI.Controllers
{
    /// <summary>
    /// claas to access Login services
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private UserService _userService;
        private readonly CustomLogger<LoginController> _logger;

        /// <summary>
        /// Constructor for Login controller
        /// </summary>
        /// <param name="loginService"></param>
        /// <param name="userService"></param>
        /// <param name="logger"></param>
        public LoginController(ILoginService loginService, IUserService userService, ILogger<LoginController> logger)
        {
            _logger = new CustomLogger<LoginController>(logger);
            _loginService = (LoginService)loginService;
            _userService=(UserService)userService;
        }
        /// <summary>
        /// Destructor for LoginController
        /// </summary>
        ~LoginController()
        {
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Method to authenticate credentials
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("/login")]
        public IActionResult Authenticate([FromBody]LoginModel model)
        {
            try
            { 
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else if (_userService.IsExisted(model.UserName, out UserBasicInfoRestModel user))
                {
                    var res = _loginService.Authenticate(model, user.UserId);
                    if (res == null)
                    {
                        return BadRequest(new { message = $"Invalid Credentials, Try Again" });
                    }
                    res.User = user;
                    return Ok(res);
                }
                return BadRequest(new { message = "User is not existed" });
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
        }

#if DEBUG
        /// <summary>
        /// Developer test: Encrypt the password by passing optional secret key
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>encrypted password</returns>

        [AllowAnonymous]
        [HttpPost]
        [Route("/Encrypt")]
        public IActionResult PostEncrypt(string password, string salt = "")
        {
            //generate new salt when it is empty/null
            salt = string.IsNullOrEmpty(salt) ? StaticMethods.RandomCharacters(6) : salt;
            return Ok(new
            {
                Salt = salt,
                PasswordHash = Security.Encrypt(password, salt)
            });
        }

        /// <summary>
        /// Developer test: Decrypt the password by passing encrypted password and secret key
        /// </summary>
        /// <param name="passwordHash"></param>
        /// <param name="salt"></param>
        /// <returns>plain password</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("/Decrypt")]
        public IActionResult PostDecrypt(string passwordHash, string salt)
        {
            return Ok(new
            {
                Password = Security.Decrypt(passwordHash, salt)
            });
        }
#endif
    }
}