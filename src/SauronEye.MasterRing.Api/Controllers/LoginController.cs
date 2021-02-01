using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SauronEye.AllDomain.Domain.Contracts.Services;
using SauronEye.AllDomain.Domain.Dtos;
using SauronEye.AllDomain.Domain.Services;
using SauronEye.AllDomain.Domain.Services.App;
using SauronEye.MasterRing.Api.Model;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SauronEye.MasterRing.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private IServiceUser service;
        private string issuer = null;
        private string audience = null;
        private string key = null;
        private ServiceZabbix serviceZabbix = null;
        public LoginController(IConfiguration config)
        {
            service = new ServiceUser(config);
            issuer = config.GetSection("Issuer").Value;
            audience = config.GetSection("Audience").Value;
            key = config.GetSection("SigningKey").Value;
            serviceZabbix = new ServiceZabbix();
    }

        [AllowAnonymous]
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Post([FromBody] Login loginModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userData = await service.GetByLogin(loginModel.Username, loginModel.Password);
                    var userId = userData == null ? "-1" : userData.Username;
                    if (userId == "-1")
                    {
                        return Unauthorized();
                    }

                    var claims = GetClaims(loginModel, userData);
                    var expires = DateTime.UtcNow.AddMinutes(2);
                    var token = GetToken(claims, expires);
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    var z = await serviceZabbix.Login("U002795", "b4tata#123");

                    userData.Jwt = tokenString;
                    userData.TokenZabbix = z.Token;
                    userData.IdZabbix = z.Id;
                    await service.Save(userData);
                    Startup.AddUser(new LoggedUser { Username = loginModel.Username, LastCall = DateTime.Now });
                    return Ok(new { token = tokenString });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return BadRequest();
        }

        private dynamic GetClaims(Login loginModel, AllDomain.Domain.Entities.User userData)
        {
            return new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, loginModel.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.NameId, loginModel.Username),
                        new Claim("DhCriado", DateTime.Now.ToString(new CultureInfo("pt-BR"))),
                        new Claim(JwtRegisteredClaimNames.Email, userData.Username ?? "")
                    };

        }

        private JwtSecurityToken GetToken(dynamic claims, DateTime expires)
        {
            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                     SecurityAlgorithms.HmacSha256)
            );
            return token;
        }
    }
}
