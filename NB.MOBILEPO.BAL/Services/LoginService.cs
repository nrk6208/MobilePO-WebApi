//using LoginServiceReference;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NB.MOBILEPO.BAL.Interfaces;
using NB.MOBILEPO.BAL.Models;
using NB.MOBILEPO.BAL.RestModels;
using NB.MOBILEPO.DAL.DbModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NB.MOBILEPO.BAL.Services
{
    public class LoginService : CommonService, ILoginService
    {
        private readonly AppSettings _appSettings;
        private readonly LNOptions _lnOptions;
        private readonly UserService _userService;
        
        public LoginService(
            IOptions<AppSettings> appSettings, 
            IOptions<LNOptions> lnOptions, 
            IUserService userService, 
            MobilePoDbContext dbContext,
            IHttpContextAccessor httpContextAccessor) : base(dbContext, httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _lnOptions = lnOptions.Value;
            _userService = (UserService)userService;
        }
        ~LoginService()
        {
            GC.SuppressFinalize(this);
        }

        public UserTokenResponse Authenticate(LoginModel model, long userId)
        {
            try
            {
                bool isVerified = _userService.VerifyPassword(model.UserName, model.Password, out UserBasicInfoRestModel user);

                // return null if credentials are wrong
                if (!isVerified)
                    return null;

                // authentication successful so generate jwt token
                var key = Encoding.ASCII.GetBytes(_appSettings.TokenSecretKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                return new UserTokenResponse
                {
                    Token = tokenHandler.WriteToken(tokenHandler.CreateToken(new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, model.UserName.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                            new Claim("SupplierId", user.SupplierId.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(2),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    }))
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
