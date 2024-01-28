using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Business.Cqrs;
using Infrastructure.DbContext;
using Infrastructure.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schemes.Dtos;
using Schemes.Exceptions;
using Schemes.Token;

namespace Business.Commands;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, TokenResponse>
{
    private readonly BackendDbContext dbContext;
    private readonly JwtConfig jwtConfig;

    public TokenCommandHandler(BackendDbContext dbContext, IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
    }

    public async Task<TokenResponse> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        string hashString = string.Empty;
        var user = await dbContext.Set<User>().Where(x => x.Username == request.Model.UserName)
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

        string token = Token(user);

        return new TokenResponse()
        {
            Username = user.Username,
            Token = token,
            ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        };
    }

    private string Token(User user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret),
                SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }

    private Claim[] GetClaims(User user)
    {
        var claims = new[]
        {
            new Claim(Constants.ClaimTypes.UserId, user.UserId.ToString()),
            new Claim(Constants.ClaimTypes.Username, user.Username),
            new Claim(Constants.ClaimTypes.Role, user.Role.ToString()),
        };

        return claims;
    }
}