namespace WebAPI.IdentityExample.Contract.AuthContract;

public class UserAlreadyExistsResponse : RegisterFailedResponse
{
    public UserAlreadyExistsResponse()
    {
        Status = "AlreadyExists";
        Message = "User already exists.";
    }
}
