using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;
using Texnokaktus.ProgOlymp.Kernel.Options.Models;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services;

internal class EmailClient(IOptions<SmtpSettings> options) : IEmailClient
{
    public async Task SendEmailAsync(string recipientAddress, string subject, string bodyHtml)
    {
        using var smtpClient = GetSmtpClient();
        smtpClient.Credentials = GetSmtpClientCredentials();
        var myMailAddress = GetSenderMailAddress();

        var mailMessage = new MailMessage
        {
            From = myMailAddress,
            To =
            {
                new MailAddress(recipientAddress)
            },
            Subject = subject,
            Body = bodyHtml,
            IsBodyHtml = true
        };

        smtpClient.EnableSsl = true;

        await smtpClient.SendMailAsync(mailMessage);
    }

    private SmtpClient GetSmtpClient()
    {
        var smtpServerSettings = options.Value.Server;
        return new(smtpServerSettings.Host, smtpServerSettings.Port);
    }

    private NetworkCredential GetSmtpClientCredentials()
    {
        var smtpCredentials = options.Value.Credentials;
        return new(smtpCredentials.Username, smtpCredentials.Password);
    }

    private MailAddress GetSenderMailAddress()
    {
        var senderSettings = options.Value.Sender;
        return new(senderSettings.Email, senderSettings.Name);
    }
}
