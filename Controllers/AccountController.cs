using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.IdentityExample.Contract.AccountContract;
using WebAPI.IdentityExample.Services.IServices;

namespace WebAPI.IdentityExample.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        /// <summary>
        ///     Ioc注入
        /// </summary>
        /// <param name="accountService"></param>
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        ///     在修改密码或者修改账号名时，必须在RazorPage中做删除JWT操作并重定向至Login获取新的JWT。
        /// </summary>
        /// <param name="model">修改密码的请求，其中包括了旧密码，新密码以及用户名</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditPassword")]
        public async Task<IActionResult> EditPassword([FromBody] EditPasswordRequest model)
        {
            var editResult = await _accountService.EditPassword(model);
            return editResult.Match<IActionResult>(
                success => Ok(success),
                _ => BadRequest()
            );
        }

        /// <summary>
        ///     在修改密码或者修改账号名时，必须在RazorPage中做删除JWT操作并重定向至Login获取新的JWT。
        /// </summary>
        /// <param name="model">修改用户名的Model，注意用户名需要保持唯一性</param>
        /// <returns></returns>
        [HttpPost]
        [Route("EditName")]
        public async Task<IActionResult> EditName([FromBody] EditUserNameRequest model)
        {
            var editResult = await _accountService.EditUserName(model);
            return editResult.Match<IActionResult>(
                success => Ok(success),
                _ => BadRequest()
            );
        }
    }
}
