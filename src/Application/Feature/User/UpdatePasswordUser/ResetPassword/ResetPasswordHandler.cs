using FluentValidation;

namespace Application.Feature.User.UpdatePasswordUser.ResetPassword;

public record ResetPasswordCommand(string Email, string CommunicationServiceType);

public class ResetPasswordHandler
{
    
}

public class ResetPasswordCommandValidator: AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.CommunicationServiceType).NotEmpty().WithMessage("CommunicationServiceType is required");
    }
}