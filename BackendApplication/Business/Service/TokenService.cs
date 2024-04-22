using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Entity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Schemes.Config.Token;
using Schemes.Dto;

namespace Business.Service;

public interface ITokenService
{
    TokenResponse CreateTokenResponse(User user);
    string CreateAccessToken(IEnumerable<Claim> claims);
    string CreateRefreshToken();
    IEnumerable<Claim> GetClaims(User user);
}

public class TokenService : ITokenService
{
    private readonly JwtConfig jwtConfig;

    public TokenService(IOptionsMonitor<JwtConfig> jwtConfig)
    {
        this.jwtConfig = jwtConfig.CurrentValue;
    }
    
    public string CreateRefreshToken()
    {
        var bytes = new byte[32];

        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    public TokenResponse CreateTokenResponse(User user)
    {
        IEnumerable<Claim> claims = GetClaims(user);
        string token = CreateAccessToken(claims);
        
        var accessTokenExpiration = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration);
        var refreshTokenExpiration = DateTime.Now.AddMinutes(jwtConfig.RefreshTokenExpiration);
        
        return new TokenResponse
        {
            Username = user.Username,
            Token = token,
            TokenExpiration = accessTokenExpiration,
            RefreshToken = CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };
    }


    public string CreateAccessToken(IEnumerable<Claim> claims)
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration);

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtConfig.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: claims,
            signingCredentials: signingCredentials
        );
        var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        return token;
    }


    public IEnumerable<Claim> GetClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(Constants.ClaimTypes.UserId, user.UserId.ToString()),
            new(Constants.ClaimTypes.Username, user.Username),
            new(ClaimTypes.Role, user.Role.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        return claims;
    }

    // TODO: Refresh Token and Client management SQL Project incele
}