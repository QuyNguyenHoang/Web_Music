namespace Music_Application.DTOs.Authentication
{
    public class SendEmailDto
    {
        public string ToEmail { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Message { get; set; } = "";
    }
}
