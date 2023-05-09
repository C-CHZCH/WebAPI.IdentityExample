namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     登出完成后返回的Http回应
/// </summary>
public class LogoutResponse
{
    public string Status { get; set; }
    public string Message { get; set; }
}