namespace Kanban.Domain.Services.MailKit;

public interface IEmailService
{
    Task SendPasswordResetCode(string to, string userName, int code);
}