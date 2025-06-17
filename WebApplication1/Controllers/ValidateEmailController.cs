using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Services;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValidateEmailController(IValidateEmailService validateEmailService) : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> ValidateEmail(string token)
        {
            try
            {
                var result = await validateEmailService.ValidateLoginByToken(token);
                return Ok("Email validated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });

            }
        }
    }
}