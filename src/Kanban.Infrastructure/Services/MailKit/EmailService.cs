using Kanban.Domain.Services.MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Kanban.Infrastructure.Services.MailKit;

public class EmailService(IOptions<EmailSettings> settings) : IEmailService
{
    public async Task SendPasswordResetCode(string to, string userName, int code)
    {
        MimeMessage message = new();

        message.From.Add(address: MailboxAddress.Parse(text: settings.Value.From));
        message.To.Add(address: MailboxAddress.Parse(text: to));
        message.Subject = "Recuperação de senha";

        message.Body = new TextPart(subtype: "html")
        {
            Text = $"""
                    <h2>Olá, {userName}</h2>

                    <p>Recebemos uma solicitação para redefinir sua senha.</p>

                    <p>Seu código de recuperação é:</p>

                    <h1>{code}</h1>

                    <p>Este código expira em 10 minutos.</p>

                    <p>Se você não solicitou isso, ignore este e-mail.</p>
                    """
        };

        using SmtpClient smtp = new();

        // Remover futuro
        smtp.Timeout = 60_000;
        smtp.CheckCertificateRevocation = false;

        await smtp.ConnectAsync(host: settings.Value.Host, port: settings.Value.Port, options: SecureSocketOptions.SslOnConnect);

        await smtp.AuthenticateAsync(userName: settings.Value.Username, password: settings.Value.Password);

        await smtp.SendAsync(message: message);
        await smtp.DisconnectAsync(quit: true);
    }
}