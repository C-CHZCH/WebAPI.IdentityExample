namespace WebAPI.IdentityExample.Contract.AccountContract
{
    /// <summary>
    ///     基础的请求Model，包含了在AccountService下所有请求Model都需要的用户名
    /// </summary>
    public class BaseModel
    {
        public string? username { get; set; }
    }
}
