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
        public AuthenticationService(UserManager<Users> userManager)
        {
            _userManager = userManager;
        }
      public async Task<bool> Validate_UserNameAsync(RegisterRequestDto registerRequestDto)
        {
            var userCheck = await _userManager.FindByEmailAsync(registerRequestDto.Email);
           return userCheck !=null ;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto registerRequestDto )
        {
            return "";
        }
    }
}