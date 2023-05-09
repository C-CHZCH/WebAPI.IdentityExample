using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     注册用户的HttpRequestModel
/// </summary>
public class RegisterModel
{
    /// <summary>
    ///     用户名
    /// </summary>
    [Required]
    [MinLength(4)]
    public required string Username { get; set; }

    /// <summary>
    ///     用户的邮箱
    /// </summary>
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    ///     用户密码
    /// </summary>
    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}
