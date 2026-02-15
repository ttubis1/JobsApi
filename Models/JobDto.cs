using JobsApi.Definitions;

namespace JobsApi.Models
{
    public class JobDto
    {
        public string JobName { get; set; }
        public int Copies { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string IntegratorUser { get; set; }
        public string MachineName { get; set; }
        public JobStatus JobStatus { get; set; } = JobStatus.NotStarted;
        

    }
}
