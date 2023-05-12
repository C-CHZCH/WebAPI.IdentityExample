using WebAPI.IdentityExample.Contract.HomeworkContract;

namespace WebAPI.IdentityExample.Services.IServices
{
    public interface IHomeworkService
    {
        public Task<Response> CreateHomework(CreateHomeworkRequest model);
        public Task<List<ClassAllHomeworkResponse>> ClassAllHomework(ClassAllHomeworkRequest model);
        public Task<Response> UpdateHomework(UpdateHomeworkRequest model);
        public Task<Response> DeleteHomework(DeleteHomeworkRequest model);
    }
}
