using Microsoft.AspNetCore.Identity;

namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     注册成功的Http回应
/// </summary>
public class RegisterSuccessResponse : AuthResponse
{
    /// <summary>
    ///     返回User的一些基本信息
    /// </summary>
    public UserResponseData User { get; init; }

    public RegisterSuccessResponse(IdentityUser user)
    {
        Status = "RegisterSuccess";
        Message = "Successfully registered user.";

        User = new()
        {
            Email = user.Email!,
            Username = user.UserName!,
            SecurityStamp = user.SecurityStamp!,
        };
    }
}
