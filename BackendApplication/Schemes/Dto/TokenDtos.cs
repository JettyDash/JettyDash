namespace Schemes.Dto;

public class TokenRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class TokenResponse
{
    public string Username { get; set; }
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}