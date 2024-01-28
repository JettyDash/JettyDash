using FluentValidation;
using FluentValidation.Validators;
using Schemes.Dtos;
using Schemes.Enums;

namespace Business.Validators;


public class ConnectionRequestBaseValidator<T>: AbstractValidator<T> where T : ConnectionRequestBase
{
    public ConnectionRequestBaseValidator()
    {
        
        RuleFor(x => x.ConnectionType)
            // .NotEmpty().WithMessage("ConnectionType is required")
            .IsEnumName(typeof(ConnectionType))
            .WithMessage("ConnectionType must be either Host or Url");
            
        RuleFor(x => x.DatabaseName)
            // .NotEmpty().WithMessage("Database name is required")
            .MaximumLength(255).WithMessage("Database name must be less than 255 characters");

        RuleFor(x => x.DatabaseType)
            // .NotEmpty().WithMessage("DatabaseType is required")
            .IsEnumName(typeof(ConnectionType))
            .WithMessage($"DatabaseType must be either {string.Join(", ", Enum.GetNames(typeof(DatabaseType)))}");

        RuleFor(x => x.Username);
            // .NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.Password);
        // .NotEmpty().WithMessage("Password is required");
    }
}




public class CreateUrlConnectionRequestValidator : AbstractValidator<CreateUrlConnectionRequest>
{
    public CreateUrlConnectionRequestValidator()
    {
        Include(new ConnectionRequestBaseValidator<CreateUrlConnectionRequest>());
        
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Url is required")
            .MaximumLength(255).WithMessage("Url must be less than 255 characters");
    }
}

public class CreateHostConnectionRequestValidator : ConnectionRequestBaseValidator<CreateHostConnectionRequest>
{
    public CreateHostConnectionRequestValidator()
    {
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
        Include(new CreateUrlConnectionRequestValidator());
        
    }
}