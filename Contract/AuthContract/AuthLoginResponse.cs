namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     登录成功
/// </summary>
public class AuthLoginResponse : AuthResponse
{
    /// <summary>
    ///     存放JWT
    /// </summary>
    public required string Token { get; init; }

    /// <summary>
    ///     生成日期
    /// </summary>
    public required DateTime ValidTo { get; init; }

    public AuthLoginResponse()
    {
        Status = "LoginSuccess";
        Message = "Successfully logged in.";
    }
}
