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

        [HttpGet("test")]
        public async Task<IActionResult> testeEmail()
        {
            var link = "<a href=\"https://sisand-app-cvghg2hxe6djamh2.canadacentral-01.azurewebsites.net/User/1\">Abrir Perfil</a>";
            emailService.SendEmailAsync("jrinho22@gmail.com", "any", "any22", link);
            //try
            //{
            //    var result = await userService.GetUserDataById("1");
            return Ok("ok");
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex);
            //}
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
