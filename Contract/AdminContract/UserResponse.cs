namespace WebAPI.IdentityExample.Contract.AdminContract;

public class UserResponse : Response
{
    public UserResponse()
    {
        Status = "Success";
        Message = "OK";
    }

    public string? UserName { get; set; }

    /// <summary>
    /// </summary>
    public string UserRoleName { get; set; }

    public Guid UserClassId { get; set; }
    public string? UserClassName { get; set; }
}