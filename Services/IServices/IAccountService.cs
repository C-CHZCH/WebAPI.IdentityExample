using OneOf;
using OneOf.Types;
using WebAPI.IdentityExample.Contract.AccountContract;

namespace WebAPI.IdentityExample.Services.IServices;

public interface IAccountService
{
    public Task<OneOf<Response, None>> EditPassword(EditPasswordRequest model);
    public Task<OneOf<Response, None>> EditUserName(EditUserNameRequest model);
    public Task<OneOf<Response, None>> JoinClass(JoinClassRequest model);
}