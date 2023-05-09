using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Model
{
    /// <summary>
    ///     班级成员
    /// </summary>
    public class ClassMember
    {
        [Key] public string Id { get; set; }

        [Required] public string Name { get; set; }

        /// <summary>
        ///     成员在班级中的角色（User或Teacher）
        /// </summary>
        [Required] public string Role { get; set; }

        public ICollection<ClassMemberMapping> ClassesMappings { get; set; }
    }
}
