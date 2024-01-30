using FluentValidation;
using FluentValidation.Validators;
using Schemes.Dtos;
using Schemes.Enums;

namespace Business.Validators;

public class CreateConnectionRequestBaseValidator<T> : AbstractValidator<T> where T : CreateConnectionRequestBase
{
    public CreateConnectionRequestBaseValidator()
    {
        RuleFor(x => x.DatabaseName)
            .NotEmpty().WithMessage("Database name is required")
            .MaximumLength(255).WithMessage("Database name must be less than 255 characters");

        RuleFor(x => x.DatabaseType)
            .NotEmpty().WithMessage("DatabaseType is required")
            .IsEnumName(typeof(ConnectionType))
            .WithMessage($"DatabaseType must be either {string.Join(", ", Enum.GetNames(typeof(DatabaseType)))}");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(255).WithMessage("Username must be less than 255 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MaximumLength(255).WithMessage("Password be less than 255 characters");

    }
}


public class UpdateConnectionRequestBaseValidator<T> : AbstractValidator<T> where T : UpdateConnectionRequestBase
{
    public UpdateConnectionRequestBaseValidator()
    {
        // check enum working as expecrted
        // if value is not null then run validations if null then its ok        
        // TODO: Make Update nullable
        RuleFor(x => x.DatabaseName)
            // .NotEmpty().WithMessage("Database name is required")
            .MaximumLength(255).WithMessage("Database name must be less than 255 characters");

        RuleFor(x => x.DatabaseType)
            // .NotEmpty().WithMessage("DatabaseType is required")
            .IsEnumName(typeof(ConnectionType))
            .WithMessage($"DatabaseType must be either {string.Join(", ", Enum.GetNames(typeof(DatabaseType)))}");

        RuleFor(x => x.Username)
            // .NotEmpty().WithMessage("Username is required")
            .MaximumLength(255).WithMessage("Username must be less than 255 characters");

        RuleFor(x => x.Password)
            // .NotEmpty().WithMessage("Password is required")
            .MaximumLength(255).WithMessage("Password be less than 255 characters");
    }
}

public class CreateUrlConnectionRequestValidator : AbstractValidator<CreateUrlConnectionRequest>
{
    public CreateUrlConnectionRequestValidator()
    {
        Include(new CreateConnectionRequestBaseValidator<CreateUrlConnectionRequest>());

        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .MaximumLength(255).WithMessage("Url must be less than 255 characters");
    }
}

public class CreateHostConnectionRequestValidator : AbstractValidator<CreateHostConnectionRequest>
{
    public CreateHostConnectionRequestValidator()
    {
        Include(new CreateConnectionRequestBaseValidator<CreateHostConnectionRequest>());

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
        Include(new UpdateConnectionRequestBaseValidator<UpdateUrlConnectionRequest>());

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
        Include(new UpdateConnectionRequestBaseValidator<UpdateHostConnectionRequest>());

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