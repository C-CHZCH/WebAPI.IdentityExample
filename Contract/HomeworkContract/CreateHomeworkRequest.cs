using System.ComponentModel.DataAnnotations;

namespace WebAPI.IdentityExample.Contract.HomeworkContract
{
    public class CreateHomeworkRequest
    {
        [Required] public Guid ClassId { get; set; }
        [MaxLength(100)] public string Name { get; set; }
        [MaxLength(256)] public string Detail { get; set; }

        public IFormFile File { get; set; }

        public int Time { get; set; }
    }
}
