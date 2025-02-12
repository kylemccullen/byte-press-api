using BytePress.Shared.Configuration;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace API.Core.Services.Email;

public class EmailSenderService(IOptions<AppSettings> appSettings) : IEmailSender
{
    private readonly AppSettings _appSettings = appSettings.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (_appSettings.EmailSettings.IsEnabled == false)
        {
            return;
        }

        var client = new SmtpClient
        {
            Host = _appSettings.EmailSettings.Host,
            Port = _appSettings.EmailSettings.Port,
            EnableSsl = _appSettings.EmailSettings.EnableSsl,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_appSettings.EmailSettings.Sender,
                _appSettings.EmailSettings.SenderPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_appSettings.EmailSettings.FromEmail),
            To = { email },
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        await client.SendMailAsync(mailMessage);
    }
}
