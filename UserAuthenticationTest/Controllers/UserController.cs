using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAuthenticationTest.Models;
using UserAuthenticationTest.Services.Interfaces;
using UserAuthenticationTest.ViewModels;

namespace UserAuthenticationTest.Controllers
{
    [Route("users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IServiceProvider serviceProvider)
        {
            _userService = serviceProvider.GetRequiredService<IUserService>();
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = await _userService.GetUserDetailsAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostUserAsync([FromBody] UserViewModel userViewModel)
        {
            var result = await _userService.AddUserAsync(userViewModel);
            return Ok(result);
        }
    }
}
