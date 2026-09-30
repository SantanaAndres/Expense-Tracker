using Application.Abstraction.Services;
using Application.Helper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Feature.User.UpdatePasswordUser.ResetPassword;

public record ResetPasswordCommand(string Email, string CommunicationServiceType);

public class ResetPasswordHandler(CommunicationServiceFactory  factory, ILogger<ResetPasswordHandler> logger)
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var communicationService = factory.GetCommunicationService(command.CommunicationServiceType);

        var parameters = new CommunicationServiceParameters("Please use this link to reset your password", "Reset your password", command.Email );
        
        var result = await communicationService.SendMessage(parameters);
        
    }
}

public class ResetPasswordCommandValidator: AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.CommunicationServiceType).NotEmpty().WithMessage("CommunicationServiceType is required");
    }
}