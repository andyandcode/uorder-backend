using Models.Accounts;
using System.Security.Claims;

namespace Application.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(AccountVm modal);

        ClaimsPrincipal ValidateToken(string token);
    }
}