using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.HomeworkContract
{
    public class ClassAllHomeworkRequest
    {
        [Required] public Guid ClassId { get; set; }
    }
}
