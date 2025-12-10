using GLeadGenerator.Contract.Emails;
using GLeadGenerator.Service.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace GLeadGenerator.Service.Emails
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(EmailSettings emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings;
            _logger = logger;
        }

        public async Task SendAsync(EmailDetailsDto details)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse(_emailSettings.Username));
            email.To.Add(MailboxAddress.Parse(_emailSettings.Recipient));
            email.Subject = "New lead from LeadGenerator";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = MakeBody(details)
            };

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailSettings.Username,
                _emailSettings.Password);

            await smtp.SendAsync(email);
            _logger.LogInformation("Email notification sent for user: {firstName} {lastName}",
                details.UserFirstName, details.UserLastName);

            await smtp.DisconnectAsync(true);
        }

        private static string MakeBody(EmailDetailsDto details)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<h1>A new lead has been created.</h1>")
              .AppendLine($"<p>Name: {details.UserFirstName} {details.UserLastName}</p>")
              .AppendLine($"<p>Email: {details.UserEmail}</p>")
              .AppendLine($"<p>Phone: {details.UserPhone}</p>")
              .AppendLine($"<p>Website: {details.UserWebsite}</p>")
              .AppendLine($"<p>Street: {details.UserStreet}</p>")
              .AppendLine($"<p>Suite: {details.UserSuite}</p>")
              .AppendLine($"<p>City: {details.UserCity}</p>")
              .AppendLine($"<p>Zip code: {details.UserZipcode}</p>")
              .AppendLine($"<p>Geolocation: {details.UserLatitude}, {details.UserLongitude}</p>");

            return sb.ToString();
        }
    }
}

