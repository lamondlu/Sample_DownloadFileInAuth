using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DownloadFileWithJWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private Settings _jwtSetting;

        public TokenController(IOptions<Settings> jwtSettingAccessor)
        {
            _jwtSetting = jwtSettingAccessor.Value;
        }

        [HttpGet]
        [Route("~/api/token")]
        public IActionResult Token()
        {
            var claims = new List<Claim> { };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                            _jwtSetting.Issuer,
                            _jwtSetting.Audience,
                            claims,
                            DateTime.Now,
                            DateTime.Now.AddMinutes(10),
                            creds);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}