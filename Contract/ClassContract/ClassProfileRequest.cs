namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     User获取班级基本信息的HttpRequest
    /// </summary>
    public class ClassProfileRequest
    {
        /// <summary>
        ///     User的Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///     班级的Id
        /// </summary>
        public Guid ClassId { get; set; }
    }
}
