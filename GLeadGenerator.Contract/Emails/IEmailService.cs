namespace GLeadGenerator.Contract.Emails;

public interface IEmailService
{
    Task SendAsync(EmailDetailsDto details);
}

