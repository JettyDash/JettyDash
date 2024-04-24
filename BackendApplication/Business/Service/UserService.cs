using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Schemes.Exception;

namespace Business.Service;


public interface IUserService
{
    int GetId();
    string GetRole();
    string GetUsername();
    
}


public class UserService(IHttpContextAccessor httpContextAccessor) : IUserService
{
    public string GetRole()
    {
        var role = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        
        if (role == null)
        {
            throw new HttpException(Constants.ErrorMessages.RoleNotFound, 404);
        }

        return role;
    }

    public string GetUsername()
    {
        var username = httpContextAccessor.HttpContext?.User.FindFirst(Constants.ClaimTypes.Username)?.Value;
        
        if (username == null)
        {
            throw new HttpException(Constants.ErrorMessages.UsernameNotFound, 404);
        }

        return username;
    }

    public int GetId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(Constants.ClaimTypes.UserId)?.Value;
        
        if (userId == null)
        {
            throw new HttpException(Constants.ErrorMessages.UserIdNotFound, 404);
        }
        return int.Parse(userId);
    }
}