namespace WebAPI.IdentityExample.Model
{
    /// <summary>
    ///     班级与成员的映射表，展现多对多关系（因为成员有可能是Teacher身份，因此需要多对多关系）
    /// </summary>
    public class ClassMemberMapping
    {
        public Guid ClassId { get; set; }
        public Classes Class { get; set; }

        public string UserId { get; set; }
        public ClassMember User { get; set; }
    }
}
