using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using MultimediaAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MultimediaAPI.Services;

namespace MultimediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        public LoginController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        public JsonResult Login(User login)
        {
            List<string> msg = _userService.ValidateLogin(login.UserName, login.Password);
            if (msg.Count == 0)
            {
                User user = new()
                {
                    UserName = login.UserName,
                    Password = login.Password,
                    EmailAddress = _userService.GetUser(login.UserName).EmailAddress,
                    IsRoot = _userService.IsRoot(login.UserName),
                };
                return new JsonResult(GenerateJsonWebToken(user));
            }
            return new JsonResult(msg);
        }
        private string GenerateJsonWebToken([FromBody] User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim("isroot", user.IsRoot.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
