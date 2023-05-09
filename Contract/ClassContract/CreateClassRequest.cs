using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     创建班级的Http请求
    /// </summary>
    public class CreateClassRequest
    {
        /// <summary>
        ///     班级名
        /// </summary>
        [Required] [MaxLength(30)] public string ClassName { get; set; }

        /// <summary>
        ///     班级的基本概述
        /// </summary>
        [Required] [MaxLength(256)] public string ClassDescription { get; set; }

        /// <summary>
        ///     班级的邀请码，建议不给予修改班级邀请码的接口
        /// </summary>
        [Required] [MaxLength(30)] public string ClassCode { get; set; }
    }
}
