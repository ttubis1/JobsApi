using JobsApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobsApi.Data.Repositories
{
    public interface IJobRepository
    {
        List<Job> GetJobs();
        Job GetJobById(int id);
        List<Job> GetJobsByUser(string user);
        List<Job> GetJobsByDateAndUser(string user, DateTime date);
        List<Job> GetJobsByDateRange([FromQuery] string user, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate);
        Job AddJob([FromBody] Job job);
        Task<List<Job>> AddJobsAsync(List<Job> jobs);
    }
}
