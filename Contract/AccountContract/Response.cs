namespace WebAPI.IdentityExample.Contract.AccountContract
{
    /// <summary>
    ///     Http回应Model
    /// </summary>
    public class Response
    {
        /// <summary>
        ///     此次操作的关键信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     此次操作服务器的状态
        /// </summary>
        public string Status { get; set; }
    }
}
