using EdiplanDotnetAPI.Application.Models.Mail;

namespace EdiplanDotnetAPI.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
