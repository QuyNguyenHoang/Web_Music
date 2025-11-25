using Microsoft.AspNetCore.Mvc;
using Music_Application.DTOs;
using Music_Application.DTOs.Authentication;
using Music_Application.Interfaces;


namespace Music_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authen;
        public AuthenticationController(IAuthentication authen)
        {
            _authen = authen;
        }
        [HttpPost("validate-username")]
        public async Task<IActionResult> ValidateUserName([FromBody] RegisterRequestDto registerRequestDto)
        {
            bool result = await _authen.Validate_UserNameAsync(registerRequestDto);
            return Ok(new { result });
        }
    }
}