namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     注册失败的Http回应
/// </summary>
public class RegisterFailedResponse : AuthResponse
{
    public RegisterFailedResponse()
    {
        Status = "RegisterFailed";
        Message = "Failed to create user account.";
    }
}
