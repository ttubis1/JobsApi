using JobsApi.Definitions;

namespace JobsApi.Models
{
    public class Job
    {
        public int JobId { get; set; }
        public string JobName { get; set; }
        public int Copies { get; set; }
        public int Printed {  get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string IntegratorUser { get; set; }
        public string MachineName { get; set; }
        public JobStatus JobStatus { get; set; } = JobStatus.NotStarted;
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; } = DateTime.MinValue;

    }
}
