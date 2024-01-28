using FluentValidation;
using Schemes.Dtos;

namespace Business.Validators;

public class CreateTokenValidator : AbstractValidator<TokenRequest>
{
    public CreateTokenValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(255);
    }
}