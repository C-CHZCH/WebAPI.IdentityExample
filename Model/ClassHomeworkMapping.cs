using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Model
{
    /// <summary>
    ///     Class与Homework的映射表，用于多对多关系的构建
    /// </summary>
    public class ClassHomeworkMapping
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();

        public Guid ClassId { get; set; }

        /// <summary>
        ///     Homework的Guid
        /// </summary>
        public Guid HomeworkId { get; set; }

        /// <summary>
        ///     Homework实体
        /// </summary>
        public Homework homework { get; set; }

        /// <summary>
        ///     Class实体
        /// </summary>
        public Classes Class { get; set; }
    }
}
