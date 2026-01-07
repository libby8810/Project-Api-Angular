using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace StoreApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpOptions _options;

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
            _logger = logger;

            // Read SMTP options from configuration (appsettings.json) under "Smtp"
            _options = new SmtpOptions
            {
                Host = _config["Smtp:Host"] ?? "localhost",
                Port = int.TryParse(_config["Smtp:Port"], out var p) ? p : 25,
                EnableSsl = bool.TryParse(_config["Smtp:EnableSsl"], out var s) ? s : false,
                Username = _config["Smtp:Username"],
                Password = _config["Smtp:Password"],
                From = _config["Smtp:From"] ?? "no-reply@store.local"
            };
        }

        public async Task<bool> SendGiftWonEmailAsync(string toEmail, string giftName)
        {
            try
            {
                using var message = new MailMessage();
                message.From = new MailAddress(_options.From);
                message.To.Add(new MailAddress(toEmail));
                message.Subject = $"מזל טוב — זכתה במתנה: {giftName}";
                message.Body = $@"שלום,

מזל טוב! זכיתם במתנה: {giftName}.

אנא השיבו להודעה זו או צרו קשר עם התמיכה כדי לארגן את קבלת המתנה.

בברכה,
צוות החנות";
                message.IsBodyHtml = false;

                using var client = new SmtpClient(_options.Host, _options.Port)
                {
                    EnableSsl = _options.EnableSsl
                };

                if (!string.IsNullOrEmpty(_options.Username))
                {
                    client.Credentials = new NetworkCredential(_options.Username, _options.Password);
                }
                else
                {
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;
                }

                // Send mail async
                await client.SendMailAsync(message);
                _logger.LogInformation("Winner email sent to {Email} for gift {Gift}", toEmail, giftName);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send winner email to {Email}", toEmail);
                return false;
            }
        }

        private class SmtpOptions
        {
            public string Host { get; set; } = "localhost";
            public int Port { get; set; } = 25;
            public bool EnableSsl { get; set; } = false;
            public string? Username { get; set; }
            public string? Password { get; set; }
            public string From { get; set; } = "chany@gmail.com";
        }
    }
}