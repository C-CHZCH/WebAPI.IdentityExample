using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Model
{
    public class Classes
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        ///     班级名
        /// </summary>
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        /// <summary>
        ///     班级描述
        /// </summary>
        [MaxLength(256)] public string Description { get; set; }

        /// <summary>
        ///     加入代码
        /// </summary>
        [MaxLength(30)]
        [Required]
        public string Code { get; set; }

        /// <summary>
        ///     人数
        /// </summary>
        public int Number { get; set; } = 0;

        public DateTime CreateOn { get; set; } = DateTime.Now;
        public DateTime UpdateOn { get; set; }

        /// <summary>
        ///     是否被锁定
        /// </summary>
        public bool IsLockout { get; set; } = false;

        public ICollection<ClassMemberMapping> MemberMappings { get; set; }

        public ICollection<ClassHomeworkMapping> ClassHomeworkMappings { get; set; }
    }
}
