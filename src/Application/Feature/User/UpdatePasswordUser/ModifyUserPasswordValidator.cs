using FluentValidation;

namespace Application.Feature.User.UpdatePasswordUser;

public class ModifyUserPasswordValidator: AbstractValidator<ModifyUserPasswordCommand>
{
    public ModifyUserPasswordValidator()
    {
        RuleFor(user => user.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .Matches(@"[\W_]").WithMessage("Password must contain at least one special character.");
    }
}