using Music_Application.DTOs.Authentication;

namespace Music_Application.Interfaces
{
    public interface IAuthentication
    {
        //Register
       
        Task<bool> Validate_UserNameAsync(RegisterRequestDto registerRequestDto);
        Task<bool> Validate_EmailAsync(RegisterRequestDto registerRequestDto);
        Task <string> RegisterAsync (RegisterRequestDto registerRequestDto);
    }
}