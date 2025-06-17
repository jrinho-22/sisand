using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.User;
using WebApplication1.Core;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(
        IUserService userService,
        IEmailService emailService) : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await userService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(long userId)
        {
            try
            {
                var result = await userService.DeleteUserById(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost()]
        public async Task<IActionResult> RegisterUsers([FromBody] RegisterViewModel registerViewModel)
        {
            try
            {
                var result = await userService.RegisterUser(registerViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(long userId, [FromBody] EditViewModel editViewModel)
        {
            try
            {
                var result = await userService.UpdateUser(editViewModel, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
