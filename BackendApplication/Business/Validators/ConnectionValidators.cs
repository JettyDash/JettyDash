using FluentValidation;
using FluentValidation.Validators;
using Schemes.Dtos;
using Schemes.Enums;

namespace Business.Validators;

public class ConnectionRequestBaseValidator<T> : AbstractValidator<T> where T : ConnectionRequestBase
{
    public ConnectionRequestBaseValidator(bool canNullable = false)
    {
        // check enum working as expecrted
        // if canNullable is true, then skip validation if value is null
        // if canNullable is false, then run all validations even if value is null

        RuleFor(x => x.ConnectionType)
            .NotEmpty().WithMessage("ConnectionType is required")
            .When(x => !canNullable) // if canNullable is true, then skip validation if value is null
            .IsEnumName(typeof(ConnectionType))
            .WithMessage("ConnectionType must be either Host or Url");

        RuleFor(x => x.DatabaseName)
            .NotEmpty().WithMessage("Database name is required")
            .When(x => !canNullable)
            .MaximumLength(255).WithMessage("Database name must be less than 255 characters");

        RuleFor(x => x.DatabaseType)
            .NotEmpty().WithMessage("DatabaseType is required")
            .When(x => !canNullable)
            .IsEnumName(typeof(ConnectionType))
            .WithMessage($"DatabaseType must be either {string.Join(", ", Enum.GetNames(typeof(DatabaseType)))}");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .When(x => !canNullable);


        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .When(x => !canNullable);
    }
}

public class CreateUrlConnectionRequestValidator : AbstractValidator<CreateUrlConnectionRequest>
{
    public CreateUrlConnectionRequestValidator()
    {
        Include(new ConnectionRequestBaseValidator<CreateUrlConnectionRequest>(canNullable: false));

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .MaximumLength(255).WithMessage("Url must be less than 255 characters");
    }
}

public class CreateHostConnectionRequestValidator : ConnectionRequestBaseValidator<CreateHostConnectionRequest>
{
    public CreateHostConnectionRequestValidator()
    {
        Include(new ConnectionRequestBaseValidator<CreateHostConnectionRequest>(canNullable: false));

        RuleFor(x => x.Host)
            .NotEmpty().WithMessage("Host is required")
            .MaximumLength(255).WithMessage("Host must be less than 255 characters");

        RuleFor(x => x.Port)
            .NotEmpty().WithMessage("Port is required")
            .GreaterThan(0).WithMessage("Port must be greater than 0");

        RuleFor(x => x.DatabaseOrSchema)
            .NotEmpty().WithMessage("Database or schema is required")
            .MaximumLength(255).WithMessage("Database or schema must be less than 255 characters");
    }
}

public class UpdateUrlConnectionRequestValidator : AbstractValidator<UpdateUrlConnectionRequest>
{
    public UpdateUrlConnectionRequestValidator()
    {
        Include(new ConnectionRequestBaseValidator<UpdateUrlConnectionRequest>(canNullable: true));

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .When(x => x.Url != null)  // Apply validation only if Url is not null
            .MaximumLength(255).WithMessage("Url must be less than 255 characters");
    }
}

public class UpdateHostConnectionRequestValidator : AbstractValidator<UpdateHostConnectionRequest>
{
    public UpdateHostConnectionRequestValidator()
    {
        Include(new ConnectionRequestBaseValidator<UpdateHostConnectionRequest>(canNullable: true));

        RuleFor(x => x.Host)
            .NotEmpty().WithMessage("Host is required")
            .When(x => x.Host != null)  // Apply validation only if Host is not null
            .MaximumLength(255).WithMessage("Host must be less than 255 characters");

        RuleFor(x => x.Port)
            .NotEmpty().WithMessage("Port is required")
            .GreaterThan(0).WithMessage("Port must be greater than 0")
            .When(x => x.Port != null); // Apply validation only if Port is not null

        RuleFor(x => x.DatabaseOrSchema)
            .NotEmpty().WithMessage("Database or schema is required")
            .When(x => x.DatabaseOrSchema != null)  // Apply validation only if DatabaseOrSchema is not null
            .MaximumLength(255).WithMessage("Database or schema must be less than 255 characters");
    }
}