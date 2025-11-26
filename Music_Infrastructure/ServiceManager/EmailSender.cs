using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Logging;
using Music_Application.DTOs.Authentication;
using Microsoft.Extensions.Options;

namespace Music_Infrastructure.Services
{
    public class EmailSender
{
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IOptions<EmailSettings> emailSettings, ILogger<EmailSender> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }

    public async Task SendEmailAsync(SendEmailDto dto)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("My App", _emailSettings.FromEmail));
        emailMessage.To.Add(MailboxAddress.Parse(dto.ToEmail));
        emailMessage.Subject = dto.Subject ?? "No Subject";

        var bodyBuilder = new BodyBuilder { HtmlBody = dto.Message ?? "" };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);

            _logger.LogInformation($"Email sent to {dto.ToEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email to {dto.ToEmail}: {ex.Message}");
            throw;
        }
    }
}
}
