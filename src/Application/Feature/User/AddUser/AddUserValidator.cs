using FluentValidation;

namespace Application.Feature.User.AddUser;

public class AddUserValidator : AbstractValidator<AddUserCommand>
{
    public AddUserValidator()
    {
        RuleFor(user => user.email).NotEmpty().WithMessage("Email is required");
        RuleFor(user => user.email).EmailAddress().WithMessage("Email is not valid");
        RuleFor(user => user.password).Must(BeValidPassword).WithMessage("Password must be at least 6 characters and contain at least one special character");
    }

    private static bool BeValidPassword(string password)
    {
        return password.Length >= 6 && password.Any(c => !char.IsLetterOrDigit(c));
    }
}
