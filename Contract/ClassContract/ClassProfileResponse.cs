namespace WebAPI.IdentityExample.Contract.ClassContract
{
    /// <summary>
    ///     返回班级基本信息的Http回应
    /// </summary>
    public class ClassProfileResponse : Response
    {
        /// <summary>
        ///     班级名
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        ///     班级的简要概述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     班级人数
        /// </summary>
        public int ClassNumber { get; set; }
    }
}
