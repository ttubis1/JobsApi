using JobsApi.Data.Repositories;
using JobsApi.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobsApi.Services
{
    public class JobService
    {
        private readonly IJobRepository _repository;

        public JobService(IJobRepository repository)
        {
            _repository = repository;
        }

        public List<Job> GetAllJobs() => _repository.GetJobs();
        public Job GetJob(int id) => _repository.GetJobById(id);
        public List<Job> GetJobByUser(string user) => _repository.GetJobsByUser(user);
        public List<Job> GetJobsByDateAndUser(string user, DateTime date) => _repository.GetJobsByDateAndUser(user, date);
        public List<Job> GetJobsByDateRange([FromQuery] string user, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate) => _repository.GetJobsByDateRange(user,startDate,endDate);
        public Job AddJob([FromBody] Job job) => _repository.AddJob(job);
        public Task AddJobs(List<Job> jobs) => _repository.AddJobsAsync(jobs);
    }
}
