using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Music_Application.DTOs;
using Music_Application.DTOs.Authentication;
using Music_Application.Interfaces;
using Music_Domain.Entities;


namespace Music_Infrastructure.Services
{
    public class AuthenticationService : IAuthentication
    {
        private readonly UserManager<Users> _userManager;
        private readonly EmailSender _emailSender;
        public AuthenticationService(UserManager<Users> userManager, EmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        public async Task<bool> Validate_UserNameAsync(RegisterRequestDto registerRequestDto)
        {
            var userCheck = await _userManager.FindByNameAsync(registerRequestDto.UserName);
            return userCheck != null;
        }
        public async Task<bool> Validate_EmailAsync(RegisterRequestDto registerRequestDto)
        {
            var emailCheck = await _userManager.FindByEmailAsync(registerRequestDto.Email);
            return emailCheck != null;
        }
        public async Task<string> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            var newUser = new Users
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.Email,
            };
            var addUser = await _userManager.CreateAsync(newUser, registerRequestDto.Password);
            if (!addUser.Succeeded)
            {
                var errors = string.Join(";", addUser.Errors.Select(e => e.Description));
                return $"Register failed:{errors}";
            }


            //Create token xac nhan email
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var confirmationUrl =  "http://localhost:5042/api/Authentication/confirmemail";
            var confirmationLink = $"{confirmationUrl}?userId={newUser.Id}&token={Uri.EscapeDataString(token)}";
            // Gửi email xác nhận
            await _emailSender.SendEmailAsync(new SendEmailDto
            {
                ToEmail = newUser.Email,
                Subject = "Confirm your email",
                Message = $"Click <a href='{confirmationLink}'>here</a> to confirm your account."
            });

            return "Register Success. Please check your email to confirm your account.";
        }

    }


}
