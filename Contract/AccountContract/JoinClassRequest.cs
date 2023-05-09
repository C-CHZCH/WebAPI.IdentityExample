using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AccountContract
{
    /// <summary>
    ///     假如班级的请求Model
    /// </summary>
    public class JoinClassRequest : BaseModel
    {
        [Required] public new string username { get; set; }

        [Required] [MaxLength(30)] public string mycode { get; set; }
    }
}
