using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SuperSimpleScheduler_Backend.Services.Interfaces;

namespace SuperSimpleScheduler_Backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    [EnableCors("AllowLocalhost")]//ALLOWS LOCAL HOST FOR DEVELOPMENT [CHANGE LATER]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        public LoginController(ILoginService loginService){
            _loginService = loginService;
        }
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var result = await _loginService.loginAsync(email, password);
            if (result is string)
            {
                return Unauthorized(result); // Invalid email or password
            }

            return Ok(result);
        }
    }

    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}