namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     有关Auth的回应Model
/// </summary>
public abstract class AuthResponse
{
    public string Status { get; init; } = "";
    public string? Message { get; init; }
}
