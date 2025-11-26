using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Music_Application.DTOs;
using Music_Application.DTOs.Authentication;
using Music_Application.Interfaces;
using Music_Domain.Entities;
using Music_Infrastructure.Services;


namespace Music_Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _authen;
        private readonly EmailSender _emailsender;
        public AuthenticationController(IAuthentication authen, EmailSender emailSender)
        {
            _authen = authen;
            _emailsender = emailSender;
        }
        [HttpPost("validate-username")]
        public async Task<IActionResult> ValidateUserName([FromBody] RegisterRequestDto registerRequestDto)
        {
            bool result = await _authen.Validate_UserNameAsync(registerRequestDto);
            return Ok(new { result });
        }
        [HttpPost("validate-email")]
        public async Task<IActionResult> ValidateEmail([FromBody] RegisterRequestDto registerRequestDto)
        {
            bool result = await _authen.Validate_EmailAsync(registerRequestDto);
            return Ok(new { result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto, string confirmationBaseUrl)
        {
            var result = await _authen.RegisterAsync(registerRequestDto);

            if (result.StartsWith("Register failed"))
            {
                return BadRequest(new
                {
                    message = result
                });
            }

            return Ok(new
            {
                message = result
            });
        }
        [HttpGet("send")]
        public async Task<IActionResult> SendTestEmail()
        {
            var testEmail = new SendEmailDto
            {
                ToEmail = "proemie.2003@gmail.com", // đổi thành email bạn muốn test
                Subject = "Test MailKit Email",
                Message = "<h3>This is a test email from MailKit</h3>"
            };

            await _emailsender.SendEmailAsync(testEmail);
            return Ok("Email sent (check inbox/spam)");
        }
        [HttpGet("confirmemail")]
public async Task<IActionResult> ConfirmEmail(
    [FromServices] UserManager<Users> userManager, 
    string userId, 
    string token)
{
    // Tìm user theo Id
    var user = await userManager.FindByIdAsync(userId);
    if (user == null) 
        return BadRequest("User not found");

    // Xác nhận email bằng token
    var result = await userManager.ConfirmEmailAsync(user, token);

    if (result.Succeeded)
        return Ok("Email confirmed successfully");

    return BadRequest("Invalid token");
}

    }

}