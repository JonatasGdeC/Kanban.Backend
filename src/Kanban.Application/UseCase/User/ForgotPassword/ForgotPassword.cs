using Kanban.Communication.Requests.User;
using Kanban.Domain.Repositories.User;
using Kanban.Domain.Security.CodeGenerator;
using Kanban.Domain.Services.MailKit;

namespace Kanban.Application.UseCase.User.ForgotPassword;
using Domain.Entities;

public class ForgotPassword(
    IUserReadRepository userReadRepository, 
    IEmailService emailService,
    ICodeGenerator codeGenerator) : IForgotPassword
{
    public async Task Execute(ForgotPasswordRequest request)
    {
        User? user = await userReadRepository.GetByEmail(email: request.Email);
        if (user != null)
        {
            await emailService.SendPasswordResetCode(to: user.Email, userName: user.Name, code: codeGenerator.Generate());
        }
    }
}