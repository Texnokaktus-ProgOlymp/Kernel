using System.Text;
using Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services.Abstractions;

namespace Texnokaktus.ProgOlymp.Kernel.Notifications.Email.Services;

internal class EmailNotificationService(IEmailClient emailClient) : IEmailNotificationService
{
    public async Task SendRegistrationSuccessfulNotificationAsync(string email, string contestUrl, string yandexIdLogin)
    {
        const string subject = "Вы успешно зарегистрированы на Олимпиаду";
        var stringBuilder = new StringBuilder().AppendParagraph("Ваша регистрация на Олимпиаду прошла успешно.")
                                               .AppendParagraph("Чтобы начать решать задачи, нажмите на указанную ниже ссылку, либо скопируйте ее в адресную строку браузера:")
                                               .AppendParagraph($"<a href=\"{contestUrl}\">{contestUrl}</a>")
                                               .AppendParagraph($"Для решения задач используйте учетную запись Яндекс, указанную в форме регистрации: <b>{yandexIdLogin}</b>.")
                                               .AppendAutomaticGeneratedEmailDisclaimer();
        await emailClient.SendEmailAsync(email, subject, stringBuilder.ToString());
    }

    public async Task SendInvalidEmailNotificationAsync(string email)
    {
        const string subject = "Не удалось завершить Вашу регистрацию на Олимпиаду.";
        var stringBuilder = new StringBuilder().AppendParagraph("Ваша регистрация на Олимпиаду не завершена.")
                                               .AppendParagraph("Адрес электронной почты, который Вы указали в регистрационной форме, неверен.")
                                               .AppendParagraph("Для регистрации на Олимпиаду необходимо использовать действующий адрес электронной почты, зарегистрированный в Яндексе. Формат: <i><b>xxx</b>@yandex.ru</i>")
                                               .AppendParagraph("Вы можете повторить попытку регистрации, заполнив форму заново.")
                                               .AppendAutomaticGeneratedEmailDisclaimer();
        await emailClient.SendEmailAsync(email, subject, stringBuilder.ToString());
    }

    public async Task SendIncorrectEmailDomainNotificationAsync(string email)
    {
        const string subject = "Не удалось завершить Вашу регистрацию на Олимпиаду.";
        var stringBuilder = new StringBuilder().AppendParagraph("Ваша регистрация на Олимпиаду не завершена.")
                                               .AppendParagraph("Адрес электронной почты, который Вы указали в регистрационной форме, не принадлежит к сервису Яндекса.")
                                               .AppendParagraph("Поскольку Олимпиада проходит на сервисе Яндекс.Контест, для участия в ней необходимо использовать учетную запись Яндекса.")
                                               .AppendParagraph("Поэтому для регистрации на Олимпиаду необходимо указать действующий адрес электронной почты, зарегистрированный в Яндексе. Формат: <i><b>xxx</b>@yandex.ru</i>")
                                               .AppendParagraph("Вы можете повторить попытку регистрации, заполнив форму заново.")
                                               .AppendAutomaticGeneratedEmailDisclaimer();
        await emailClient.SendEmailAsync(email, subject, stringBuilder.ToString());
    }

    public async Task SendYandexIdLoginDuplicateNotificationAsync(string email)
    {
        const string subject = "Не удалось завершить Вашу регистрацию на Олимпиаду.";
        var stringBuilder = new StringBuilder().AppendParagraph("Ваша регистрация на Олимпиаду не завершена.")
                                               .AppendParagraph("Адрес электронной почты, который Вы указали в регистрационной форме, уже используется другим участником Олимпиады.")
                                               .AppendParagraph("К сожалению, не получится использовать одну и ту же учетную запись Яндекса для решения задач несколькими участниками в рамках одной Олимпиады.")
                                               .AppendParagraph("Укажите другой адрес электронной почты Яндекса.")
                                               .AppendParagraph("Вы можете повторить попытку регистрации, заполнив форму заново.")
                                               .AppendAutomaticGeneratedEmailDisclaimer();
        await emailClient.SendEmailAsync(email, subject, stringBuilder.ToString());
    }
}

file static class StringBuilderExtensions
{
    public static StringBuilder AppendParagraph(this StringBuilder builder, string text) =>
        builder.AppendLine($"<p>{text}</p>");

    public static StringBuilder AppendParagraph(this StringBuilder builder, string text, string style) =>
        builder.AppendLine($"<p style=\"{style}\">{text}</p>");

    public static StringBuilder AppendAutomaticGeneratedEmailDisclaimer(this StringBuilder builder) =>
        builder.AppendParagraph("Данное письмо сгенерировано автоматически. Отвечать на него не нужно.",
                                "font-size:12px; color:gray");
}
