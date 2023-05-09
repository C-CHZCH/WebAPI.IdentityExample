using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AccountContract
{
    /// <summary>
    ///     修改用户名的请求model
    /// </summary>
    public class EditUserNameRequest : BaseModel
    {
        /// <summary>
        ///     新的名字，要确保唯一性
        /// </summary>
        [Required] public string newusername { get; set; }
    }
}
