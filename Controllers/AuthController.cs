using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.Contract.AuthContract;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Controllers
{
    /// <summary>
    ///     简单的Auth身份验证控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        /// <summary>
        ///     Ioc注入
        /// </summary>
        /// <param name="service"></param>
        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        ///     Login操作
        /// </summary>
        /// <param name="model">登录Model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var loginResult = await _service.Login(model);

            return loginResult.Match<IActionResult>(
                success => Ok(success),
                failed => Unauthorized(failed)
            );
        }

        /*/// <summary>
        ///     注册操作
        /// </summary>
        /// <param name="model">注册Model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var registerResult = await _service.Register(model);
            return registerResult.Match<IActionResult>(
                    success => Ok(success),
                    failed => BadRequest(failed)
                );
        }*/

        /// <summary>
        ///     注册角色为Teacher的用户，仅有管理员能操作
        /// </summary>
        /// <param name="model">同上注册Model</param>
        /// <returns></returns>
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        [Route("register/teacher")]
        public async Task<IActionResult> RegisterTeacher([FromBody] RegisterModel model)
        {
            var registerResult = await _service.Register(model, UserRole.Teacher);
            return registerResult.Match<IActionResult>(
                success => Ok(success),
                failed => BadRequest(failed)
            );
        }


        /// <summary>
        ///     获取指定User的Role信息，避免前端的JWT被篡改或泄露造成前端关键页面的泄露，但是这仍不安全，更多措施仍在思考。
        /// </summary>
        /// <param name="username">通过用户名获取</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("getrole")]
        public async Task<IActionResult> GetRole([FromQuery] string username)
        {
            //等待做缓存
            var result = await _service.GetRole(username);
            if (result == "User Null")
                return BadRequest();
            return Ok(result);
        }
    }
}
