using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AccountContract
{
    /// <summary>
    ///     修改密码的请求Model
    /// </summary>
    public class EditPasswordRequest : BaseModel
    {
        /// <summary>
        ///     旧密码
        /// </summary>
        [Required] public string oldpassword { get; set; }

        /// <summary>
        ///     新密码
        /// </summary>
        [Required] public string newpassword { get; set; }
    }
}
