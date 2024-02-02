using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Schemes.Exceptions;

namespace Business.Services;


public interface IUserService
{
    int GetId();
    string GetRole();
    
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