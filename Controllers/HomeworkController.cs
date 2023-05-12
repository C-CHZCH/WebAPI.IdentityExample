using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.Contract.HomeworkContract;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworkController : ControllerBase
    {
        private IHomeworkService Service { get; }

        public HomeworkController(IHomeworkService service)
        {
            Service = service;
        }

        [Route("getallhomeworkbyclassid")]
        [HttpPost]
        public async IAsyncEnumerable<ClassAllHomeworkResponse> GetAllHomeworkByClassId(
            [FromBody] ClassAllHomeworkRequest model)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                yield break;
            }

            var result = await Service.ClassAllHomework(model);
            foreach (var res in result) yield return res;
        }

        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [Route("createhomwork")]
        [HttpPost]
        public async Task<IActionResult> CreateHomework([FromBody] CreateHomeworkRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await Service.CreateHomework(model);
            return result.Status == "Success" ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [Route("deletehomework")]
        [HttpGet]
        public async Task<IActionResult> DeleteHomework([FromQuery] Guid HomeworkId)
        {
            if (HomeworkId == Guid.Empty) return BadRequest(ModelState);
            var model = new DeleteHomeworkRequest
            {
                HomeworkId = HomeworkId
            };
            var result = await Service.DeleteHomework(model);
            return result.Status == "Success" ? Ok(result) : BadRequest(result);
        }

        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [Route("updatehomework")]
        [HttpPost]
        public async Task<IActionResult> UpdateHomework([FromBody] UpdateHomeworkRequest model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await Service.UpdateHomework(model);
            return result.Status == "Success" ? Ok(result) : BadRequest(result);
        }
    }
}
