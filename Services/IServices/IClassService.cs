using WebAPI.IdentityExample.Contract.ClassContract;

namespace WebAPI.IdentityExample.Services.IServices;

public interface IClassService
{
    public Task<ClassProfileResponse> GetClassProFile(ClassProfileRequest model);
    public Task<Response> CreateClass(CreateClassRequest model);
    public Task<Response> UpdateClass(ClassUpdateResquest model);
}