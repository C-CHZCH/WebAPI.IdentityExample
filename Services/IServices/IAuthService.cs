using OneOf;
using WebAPI.IdentityExample.Contract.AuthContract;

namespace WebAPI.IdentityExample.Services.IServices;

public interface IAuthService
{
    Task<OneOf<AuthLoginResponse, AuthLoginFailureResponse>> Login(LoginModel model);
    Task<OneOf<RegisterSuccessResponse, RegisterFailedResponse>> Register(RegisterModel model, string role = UserRole.User);

    Task<string> GetRole(string username);
    Task SeedRoles();
}
