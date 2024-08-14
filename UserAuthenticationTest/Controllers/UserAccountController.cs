using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;
using UserAuthenticationTest.Services.Interfaces;

namespace UserAuthenticationTest.Controllers
{
    [Route("useraccount")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserAccountController(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
        }

        [HttpGet]
        [Route("generate-token")]
        public async Task<IActionResult> GeTokenAsync([Required][FromHeader] string login_id, [Required][FromHeader]string password)
        {
            var result =  await _userService.GenerateJwtTokenAsync(login_id, password);
            if (string.IsNullOrEmpty(result))
                return BadRequest(new { errors = "Login id or password null or empty." });

            return Ok(result);
        }
    }
}
