using WebAPI.IdentityExample.Contract.ClassContract;

namespace WebAPI.IdentityExample.Contract.AdminContract
{
    public class AllClassProfileResponse : ClassProfileResponse
    {
        public Guid ClassId { get; set; }
        public bool isLockOut { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
