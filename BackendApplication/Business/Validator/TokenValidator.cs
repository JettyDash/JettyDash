using FluentValidation;
using Schemes.DTOs;

namespace Business.Validators;

public class CreateTokenValidator : AbstractValidator<TokenRequest>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
    }
}