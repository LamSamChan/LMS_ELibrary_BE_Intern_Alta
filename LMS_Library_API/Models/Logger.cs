namespace LMS_Library_API.Models
{
    public class Logger
    {
        public TaskStatus status { get; set; }
        public string? message { get; set; } = null;
        public object data { get; set; }
        public List<object> listData { get; set; }
    }
}
