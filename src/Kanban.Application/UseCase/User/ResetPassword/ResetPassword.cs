using FluentValidation.Results;
using Kanban.Communication.Requests.User;
using Kanban.Domain.Repositories;
using Kanban.Domain.Repositories.PasswordResetCode;
using Kanban.Domain.Repositories.User;
using Kanban.Domain.Security.Cryptography;
using Kanban.Domain.Security.Tokens;
using Kanban.Exception.ExceptionBase;

namespace Kanban.Application.UseCase.User.ResetPassword;
using Domain.Entities;

public class ResetPassword(
    IVerifyTokenResetCode verifyTokenResetCode,
    IUserWriteRepository userWriteRepository, 
    IEncrypter encrypter,
    IUnitOfWork unitOfWork,
    IPasswordResetCodeRepository passwordResetCodeRepository) : IResetPassword
{
    public async Task Execute(ResetPasswordRequest request)
    {
        Validate(request: request);
        
        Guid userId = verifyTokenResetCode.GetUserId(token: request.TokenResetPassword) ?? throw new BadRequestException(message: "Invalid or expired token");
        User user = await userWriteRepository.GetById(id: userId) ?? throw new BadRequestException(message: "Invalid or expired token");
        PasswordResetCode resetCode = await passwordResetCodeRepository.GetByUserId(userId: userId) ?? throw new BadRequestException(message: "Invalid or expired token");

        user.Password = encrypter.Encrypt(value: request.NewPassword);

        userWriteRepository.Update(user: user);
        passwordResetCodeRepository.Remove(passwordResetCode: resetCode);

        await unitOfWork.Commit();
    }
    
    private void Validate(ResetPasswordRequest request)
    {
        ValidationResult resultPassword = new PasswordValidator().Validate(instance: request.NewPassword);

        if (!resultPassword.IsValid)
        {
            List<string> errors = resultPassword.Errors.Select(selector: error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorsMessages: errors);
        }
    }
}