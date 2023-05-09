using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.Contract.ClassContract;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Controllers
{
    /// <summary>
    ///     用于获取Class信息，创建Class，修改Class的控制器
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly IClassService _service;

        /// <summary>
        ///     Ioc注入
        /// </summary>
        /// <param name="service"></param>
        public ClassController(IClassService service)
        {
            _service = service;
        }


        /// <summary>
        ///     获取Class的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getclass")]
        public async Task<IActionResult> GetClass(ClassProfileRequest model)
        {
            var result = await _service.GetClassProFile(model);
            if (result.Status != "Success")
                return BadRequest();
            return Ok(result);
        }

        /// <summary>
        ///     创建一个Class，仅有管理员以及教师有此操作的权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [HttpPost]
        [Route("createclass")]
        public async Task<IActionResult> CreateClass([FromBody] CreateClassRequest model)
        {
            var result = await _service.CreateClass(model);
            if (result.Status != "Success")
                return BadRequest(result);
            return Ok(result);
        }

        /// <summary>
        ///     更新班级信息，仅有管理员或教师有此操作的权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize(Roles = UserRole.Admin + "," + UserRole.Teacher)]
        [HttpPost]
        [Route("updateclass")]
        public async Task<IActionResult> UpdateClass(ClassUpdateResquest model)
        {
            var result = await _service.UpdateClass(model);
            if (result.Status != "Success")
                return BadRequest(result);
            return Ok(result);
        }
    }
}
