using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Toyin_group_api.Core.Data;
using Toyin_group_api.Core.Entities;
using Toyin_group_api.Core.Models;
using Toyin_group_api.Core.Models.Payload;
using Toyin_group_api.Core.Models.Resources;

namespace Toyin_group_api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {

        public IConfiguration configuration;
        private readonly AppDbContext dbContext;
        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        /// <summary>
        /// Creates a new Todo
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> Post(AuthPayload payload)
        {
          
            if (payload is not null)
            {
              
                var jwt =  configuration.GetSection("Jwt").Get<Jwt>();

              var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id",Guid.NewGuid().ToString()),
                        new Claim("UserName", payload.Username),
                        new Claim("Password", payload.Password)
                    
                    };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));

            }
            else
            {
                return BadRequest( "Invalid Request");
              
            }
         
        }
    }
}
