using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AdminContract
{
    public class AllClassHomeworkResponse
    {
        public Guid Id { get; set; }

        public DateTime CreateOn { get; set; }

        /// <summary>
        ///     默认到期时间为7天
        /// </summary>
        public DateTime LasTime { get; set; }

        [MaxLength(100)] public string Name { get; set; }

        [MaxLength(256)] public string Description { get; set; }

        /// <summary>
        ///     已交人数
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     保存的地址
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }
    }
}
