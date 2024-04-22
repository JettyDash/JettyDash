using FluentValidation;
using Schemes.DTOs;
using Schemes.Enums;

// using VaultSharp.V1.SecretsEngines.Database;

namespace Business.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {

        RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required.")
            .IsEnumName(typeof(Role))
            .WithMessage($"Role must be either {string.Join(", ", Enum.GetNames(typeof(Role)))}")
            .MaximumLength(30).WithMessage("Role cannot exceed 30 characters.");
            
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 255).WithMessage("Password must be between 8 and 255 characters.")
            .Matches(@"^(?=.*[!@#$%^&*(),.?\\\"":{}|<>])(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z]).*$")
            .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .Length(2, 50).WithMessage("First Name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .Length(2, 50).WithMessage("Last Name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");


        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.")
            .IsEnumName(typeof(Role))
            .WithMessage($"Role must be either {string.Join(", ", Enum.GetNames(typeof(Role)))}")
            .MaximumLength(30).WithMessage("Role cannot exceed 30 characters.");
            
    }
}


