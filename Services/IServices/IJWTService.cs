using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPI.IdentityExample.Services.IServices;

public interface IJWTService
{
    JwtSecurityToken GetToken(IEnumerable<Claim> claims);
}
