using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.HomeworkContract
{
    public class UpdateHomeworkRequest
    {
        [Required] public Guid HomeworkId { get; set; }
        [MaxLength(100)] public string? NewHomeworkName { get; set; }
        [MaxLength(256)] public string? NewHomeworkDescription { get; set; }

    }
}
