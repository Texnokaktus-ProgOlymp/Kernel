namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;

public interface IEmailClient
{
    Task SendEmailAsync(string recipientAddress, string subject, string bodyHtml);
}
