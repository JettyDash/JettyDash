using System.Security.Cryptography;
using System.Text;
using Business.Cqrs;
using Business.Service;
using Infrastructure.DbContext;
using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Schemes.Dto;
using Schemes.Exception;
using Schemes.Mediatr;

namespace Business.Command;

public class TokenCommandHandler :
    IAsyncCommandHandler<CreateTokenCommand, TokenResponse>
{
    private readonly BackendDbContext dbContext;
    private readonly ITokenService tokenService;
    public TokenCommandHandler(BackendDbContext dbContext, ITokenService tokenService)
    {
        this.dbContext = dbContext;
        this.tokenService = tokenService;
    }

    public async Task<TokenResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        string hashString = string.Empty;
        var user = await dbContext.Set<User>().Where(x => x.Username.Equals(request.Model.Username))
            .FirstOrDefaultAsync(cancellationToken);
            
        if (user == null)
        {
            throw new HttpException(Constants.ErrorMessages.InvalidUserInformation, 400);
        }

        if (!user.IsActive)
        {
            throw new HttpException(Constants.ErrorMessages.ContactAdministrator, 403);
        }

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] data = Encoding.UTF8.GetBytes(request.Model.Password.Trim());
            byte[] hashBytest = sha256.ComputeHash(data);
            hashString = BitConverter.ToString(hashBytest).Replace("-", "");

        }

        if (hashString != user.Password)
        {
            user.LastActivityDateTime = DateTime.UtcNow;
            user.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);
            throw new HttpException($"Credentials are incorrect. You have {3 - user.PasswordRetryCount} attempts left.", 401);
        }

        if (user.PasswordRetryCount > 3)
        {
            throw new HttpException(Constants.ErrorMessages.ContactAdministrator, 405);
        }

        user.LastActivityDateTime = DateTime.UtcNow;
        user.PasswordRetryCount = 0;
        await dbContext.SaveChangesAsync(cancellationToken);

        var response = tokenService.CreateTokenResponse(user);

        return response;
    }
}