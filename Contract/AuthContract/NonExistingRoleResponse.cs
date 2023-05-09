namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     不存在这样一个Role
/// </summary>
public class NonExistingRoleResponse : RegisterFailedResponse
{
    /// <summary>
    ///     Role名
    /// </summary>
    public required string Role { get; init; }

    public NonExistingRoleResponse()
    {
        Status = "NonExistingRole";
        Message = "Role does not exist.";
    }
}
