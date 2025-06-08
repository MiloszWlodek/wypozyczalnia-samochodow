using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;

namespace CarRentalApp.Server.Services
{
    public class EmailService
    {
        readonly SmtpOptions _opt;

        public EmailService(IOptions<SmtpOptions> opt) => _opt = opt.Value;

        public async Task SendAsync(string to, string subject, string htmlContent)
        {
            var msg = new MimeMessage();
            msg.From.Add(MailboxAddress.Parse(_opt.From));
            msg.To.Add(MailboxAddress.Parse(to));
            msg.Subject = subject;
            msg.Body    = new TextPart("html") { Text = htmlContent };

            using var client = new SmtpClient();
            await client.ConnectAsync(_opt.Host, _opt.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_opt.Username, _opt.Password);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
    }
}
