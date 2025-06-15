using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.User;
using WebApplication1.Models;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetUsers(long UserId)
        {
            try
            {
                var result = await userService.GetUserDataById(UserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                return BadRequest(ex);
            }
        }
    }
}
