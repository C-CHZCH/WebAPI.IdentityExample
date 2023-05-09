namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     用于管理员添加班级成员的Http请求
    /// </summary>
    public class AddClassNumberForAdminRequest
    {
        /// <summary>
        ///     该学生的用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     该班级的班级名
        /// </summary>
        public string Classname { get; set; }

        /// <summary>
        ///     该班级的Id（邀请码Code）
        /// </summary>
        public string ClassId { get; set; }
    }
}
