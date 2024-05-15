using EdiplanDotnetAPI.Application.Contracts.Infrastructure;
using EdiplanDotnetAPI.Application.Models.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EdiplanDotnetAPI.Infrastructure.Mail;
public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }
    public ILogger<EmailService> _logger { get; }

    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }
    public async Task<bool> SendEmail(Email email)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var to = new EmailAddress(email.To);
        var subject = email.Subject;
        var emailBody = email.Body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        _logger.LogInformation($"Email sent to: {to}");

        if (response.StatusCode == System.Net.HttpStatusCode.Accepted ||
            response.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true;
        }

        _logger.LogError("Email sending failed.");
        return false;

    }
}
