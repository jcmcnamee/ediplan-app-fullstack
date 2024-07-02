using Ediplan.Application.Models.Mail;

namespace Ediplan.Application.Contracts.Infrastructure;

public interface IEmailService
{
    Task<bool> SendEmail(Email email);
}
