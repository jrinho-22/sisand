using Microsoft.AspNetCore.Mvc;
using WebApplication1.Contracts.Login;
using WebApplication1.Contracts.User;
using WebApplication1.Models.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController(ILoginService loginService) : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> RegisterUsers([FromBody] LoginViewModel loginViewModel)
        {
            try
            {
                var result = await loginService.GetUserSession(loginViewModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{loginId}")]
        public async Task<IActionResult> GetLogin(long loginId)
        {
            try
            {
                var result = await loginService.GetLoginById(loginId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
