namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     User获取班级基本信息的HttpRequest
    /// </summary>
    public class ClassProfileRequest
    {
        /// <summary>
        ///     User的账户名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        ///     班级的Id
        /// </summary>
        public string ClassCode { get; set; }
    }
}
