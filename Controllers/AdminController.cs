using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.Contract.AdminContract;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Controllers
{
    [Authorize(UserRole.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [Route("getallclass")]
        [HttpGet]
        public async IAsyncEnumerable<AllClassProfileResponse> GetAllClass()
        {
            var result = await _adminService.GetAllClassProfile();

            foreach (var r in result) yield return r;
        }

        [Route("getalluser")]
        [HttpGet]
        public async IAsyncEnumerable<UserResponse> GetAllAccount()
        {
            var result = await _adminService.GetAllAccount();
            foreach (var r in result) yield return r;
        }

        [Route("addusertoteacher")]
        [HttpPost]
        public async Task<IActionResult> AddUserToTeacherRequest(AddUsertoTeacherRequest model)
        {
            var result = await _adminService.AddUserToTeacherRequest(model);
            if (result.Status != "Success")
                return BadRequest(result);
            return Ok(result);
        }

        [Route("getallclassprofile")]
        [HttpGet]
        public async IAsyncEnumerable<AllClassProfileResponse> GetAllClassProfile()
        {
            var result = await _adminService.GetAllClassProfile();
            foreach (var r in result) yield return r;
        }

        [Route("getallhomework")]
        [HttpGet]
        public async IAsyncEnumerable<AllClassHomeworkResponse> GetAllHomework()
        {
            var result = await _adminService.AllHomeworkProfile();
            foreach (var r in result) yield return r;
        }

        [Route("editpasswordforuser")]
        [HttpPost]
        public async Task<IActionResult> EditPasswordForUser([FromBody] string username, [FromBody] string newpassword)
        {
            var result = await _adminService.EditUserPassword(username, newpassword);
            return result.Status == "Success" ? Ok(result) : BadRequest(result);
        }
    }
}
