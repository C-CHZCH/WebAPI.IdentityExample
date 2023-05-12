using Microsoft.AspNetCore.Identity;

namespace WebAPI.IdentityExample.Model
{
    /// <summary>
    ///     继承于IdentityUser，用于扩充IdentityUser
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        ///     班级名
        /// </summary>
        public string? ClassName { get; set; }

        /// <summary>
        ///     班级邀请码
        /// </summary>
        public Guid ClassId { get; set; }
    }
}
