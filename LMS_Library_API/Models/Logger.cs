namespace LMS_Library_API.Models
{
    public class Logger
    {
        public TaskStatus status { get; set; }
        public DateTime dateRequest { get; set; } = DateTime.Now;
        public string? message { get; set; } = null;
        public object data { get; set; }
        public IEnumerable<object> listData { get; set; }
    }
}
