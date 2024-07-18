using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SuperSimpleScheduler_Backend.Services;

namespace SuperSimpleScheduler_Backend.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("AllowLocalhost")]//ALLOWS LOCAL HOST FOR DEVELOPMENT [CHANGE LATER]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService){
            _userService = userService;
        }

        [HttpGet("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> GetUserById( int userId){
            var result = await _userService.GetUserByIdAsync(userId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("by-email/{userEmail}")]
        [Authorize]
        public async Task<IActionResult> GetUserByEmail( string userEmail){
            var result = await _userService.GetUserByEmailAsync(userEmail);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewUserAccount(
            [FromForm] string email, [FromForm] string password, [FromForm] string confirmPassword)
        {
            if(!string.Equals(password, confirmPassword)){
                return BadRequest("Passwords don't match");
            }

            var result = await _userService.CreateUserAsync(email, password);
            if(!(result is Models.User)){
                return BadRequest(result);
            }
            if (result==null){
                return BadRequest("result is null");
            }

            //TODO email authentication?

            return Ok(result);
        }

        [HttpPut("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAccount(
            int userId, [FromForm] string oldPassword, [FromForm] string newPassword, [FromForm] string confirmNewPassword)
        {
            if(!string.Equals(newPassword, confirmNewPassword)){
                return BadRequest("Passwords don't match");
            }

            var result = await _userService.UpdateUserByIdAsync(userId, oldPassword, newPassword);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{userId:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteUserAccount(int userId){
            var result = await _userService.DeleteUserByIdAsync(userId);
            return result == null ? NotFound() : Ok(result);
        }


    }
}