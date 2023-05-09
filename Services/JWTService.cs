using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Services;

/// <summary>
///     用于发放JWT的服务
/// </summary>
public class JWTService : IJWTService
{
    private readonly JWTServiceOptions _options;

    /// <summary>
    ///     获取JWTBearer的设置
    /// </summary>
    /// <param name="options">JWT设置</param>
    public JWTService(IOptions<JWTServiceOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    ///     生成密钥
    /// </summary>
    private byte[] SecretBytes
    {
        get => Encoding.UTF8.GetBytes(_options.Secret);
    }

    /// <summary>
    ///     JWT的生成
    /// </summary>
    /// <param name="authClaims">传入的与账户有关的Claim，用于放入JWT中</param>
    /// <returns></returns>
    public JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(SecretBytes);

        return new JwtSecurityToken(
                issuer: _options.ValidIssuer,
                audience: _options.ValidAudience,
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
    }
}
