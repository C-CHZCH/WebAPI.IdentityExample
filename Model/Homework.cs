using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.IdentityExample.Model
{
    /// <summary>
    ///     Homework
    /// </summary>
    public class Homework
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime CreateOn { get; set; } = DateTime.Now;

        /// <summary>
        ///     默认到期时间为7天
        /// </summary>
        public DateTime LasTime { get; set; } = DateTime.Now.AddDays(7);

        [MaxLength(100)] public string Name { get; set; }

        [MaxLength(256)] public string Description { get; set; }

        /// <summary>
        ///     已交人数
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     保存的地址
        /// </summary>
        [MaxLength(256)] public string Url { get; set; }

        /// <summary>
        ///     文件类
        /// </summary>
        [NotMapped] public IFormFile FormFile { get; set; }

        public ICollection<ClassHomeworkMapping> ClassHomeworkMappings { get; set; }
    }
}
