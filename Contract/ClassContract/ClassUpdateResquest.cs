using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     班级的更新请求
    /// </summary>
    public class ClassUpdateResquest
    {
        /// <summary>
        ///     是否修改班级名（若为空则不修改）
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///     是否修改班级的基本描述（若为空则不修改）
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        ///     根据邀请码来锁定目标班级
        /// </summary>
        [Required]
        public string ClassCode { get; set; }
    }
}
