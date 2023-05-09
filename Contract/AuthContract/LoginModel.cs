using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     请求登录的Model
/// </summary>
public class LoginModel
{
    /// <summary>
    ///     用户名，最短4个字节
    /// </summary>
    [Required]
    [MinLength(4)]
    public required string Username { get; set; }

    /// <summary>
    ///     密码，最短8位
    /// </summary>
    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
