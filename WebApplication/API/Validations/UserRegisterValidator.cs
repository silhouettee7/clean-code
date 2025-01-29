using API.DTO;
using FluentValidation;
namespace API.Validations;

public class UserRegisterValidator: AbstractValidator<UserRegister>
{
    public UserRegisterValidator()
    {
        RuleFor(userRegister => userRegister.UserName)
            .NotEmpty().WithMessage("Username is not required")
            .MinimumLength(3)
            .MaximumLength(25).WithMessage("Username must be between 3 and 25 characters");
        
        RuleFor(userRegister => userRegister.Password)
            .NotEmpty().WithMessage("Password is not required")
            .MinimumLength(8)
            .MaximumLength(50).WithMessage("Password must be between 8 and 50 characters")
            .Matches(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]$")
            .WithMessage("Password must be at least one digit, special symbol, and upper case letter.");

        RuleFor(userRegister => userRegister.UserEmail)
            .NotEmpty().WithMessage("Email is not required")
            .EmailAddress().WithMessage("Invalid email address");
    }
}