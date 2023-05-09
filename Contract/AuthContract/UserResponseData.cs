namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     用于在注册成功后的返回
/// </summary>
public class UserResponseData
{
    /// <summary>
    ///     邮箱
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    ///     .NET中用户的安全密钥
    /// </summary>
    public required string SecurityStamp { get; set; }
}
