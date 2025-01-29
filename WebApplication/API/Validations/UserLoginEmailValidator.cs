using API.DTO;
using FluentValidation;

namespace API.Validations;

public class UserLoginEmailValidator: AbstractValidator<UserLoginEmail>
{
    public UserLoginEmailValidator()
    {
        RuleFor(userLogin => userLogin.Password)
            .NotEmpty().WithMessage("Password is not required")
            .MinimumLength(8)
            .MaximumLength(50).WithMessage("Password must be between 8 and 50 characters")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$")
            .WithMessage("Password must be at least one digit, special symbol, and upper case letter.");

        RuleFor(userLogin => userLogin.UserEmail)
            .NotEmpty().WithMessage("Email is not required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}