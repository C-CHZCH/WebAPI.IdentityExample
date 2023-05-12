using WebAPI.IdentityExample.Contract.AdminContract;

namespace WebAPI.IdentityExample.Services.IServices
{
    public interface IAdminService
    {
        public Task<List<UserResponse>> GetAllAccount();

        public Task<Response> AddUserToTeacherRequest(AddUsertoTeacherRequest model);

        public Task<List<AllClassProfileResponse>> GetAllClassProfile();

        public Task<List<AllClassHomeworkResponse>> AllHomeworkProfile();

        public Task<Response> EditUserPassword(string username, string newpassword);
    }
}
