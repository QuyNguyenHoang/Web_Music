using Music_Application.DTOs.Authentication;

namespace Music_Application.Interfaces
{
    public interface IAuthentication
    {
        //Register
        Task<string> RegisterAsync (RegisterRequestDto registerRequestDto);
        Task<bool> Validate_UserNameAsync(RegisterRequestDto registerRequestDto);
        
    }
}