namespace WebAPI.IdentityExample.Contract.AuthContract;

/// <summary>
///     基础的Role
/// </summary>
public static class UserRole
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Teacher = "Teacher";
    public static readonly string[] Roles = new[] { Admin, User };
}
