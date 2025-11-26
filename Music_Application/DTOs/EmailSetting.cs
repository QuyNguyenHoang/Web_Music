namespace Music_Application.DTOs.Authentication
{
    public class EmailSettings
    {
        public string FromEmail { get; set; } = "";
        public string Password { get; set; } = "";
        public string SmtpServer { get; set; } = "";
        public int Port { get; set; } = 587;
    }
}