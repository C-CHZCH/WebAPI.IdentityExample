using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.AdminContract
{
    public class AddUsertoTeacherRequest
    {
        [Required] [MaxLength(100)] public string UserName { get; set; }
    }
}
