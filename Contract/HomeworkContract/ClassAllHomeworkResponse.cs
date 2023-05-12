namespace WebAPI.IdentityExample.Contract.HomeworkContract
{
    public class ClassAllHomeworkResponse : Response
    {
        public Guid HomeworkGuid { get; set; }
        public string HomewordName { get; set; }
        public string HomewordDetail { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LasTime { get; set; }
    }
}
