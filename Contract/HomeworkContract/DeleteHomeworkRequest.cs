using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.HomeworkContract
{
    public class DeleteHomeworkRequest
    {
        [Required] public Guid HomeworkId { get; set; }
    }
}
