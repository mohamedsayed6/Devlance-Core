using Devlance.Domain.Interfaces.Services;
using Devlance.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Devlance_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("RegisterAsync")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(model);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

		[HttpPost("LoginAsync")]
		public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.LoginAsync(model);

			if (!result.IsAuthenticated)
				return BadRequest(result.Message);

			return Ok(result);
		}

		[HttpPost("AssignUserToRoleAsync")]
		public async Task<IActionResult> AssignUserToRoleAsync([FromBody] AssignUserToRoleModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authService.AssignUserToRoleAsync(model);

			if (!string.IsNullOrEmpty(result))
				return BadRequest(result);

			return Ok(model);
		}
	}
}
