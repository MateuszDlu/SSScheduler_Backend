using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SuperSimpleScheduler_Backend.Models;
using SuperSimpleScheduler_Backend.Services.Interfaces;

namespace SuperSimpleScheduler_Backend.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public LoginService(IUserService userService, IConfiguration configuration){
            _userService = userService;
            _configuration = configuration;
        }
        public async Task<Object> loginAsync(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password)){
                return "invalid email or password";
            }

            var token = GenerateJwtToken(user);
            return new {Token = token, User = user};
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(180),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}