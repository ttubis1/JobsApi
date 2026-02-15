using JobsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace JobsApi.Data.Repositories
{
    public class JobsRepositoryDb(IJobsDbContext jobsDbContext) : IJobRepository
    {
        public Job AddJob([FromBody] Job job)
        {
            jobsDbContext.Jobs.Add(job);
            jobsDbContext.SaveChangesAsync();
            return job;
        }

        public async Task<List<Job>> AddJobsAsync(List<Job> jobs)
        {
            // Add jobs to the EF Core DbContext
            jobsDbContext.Jobs.AddRange(jobs);

            // Save changes asynchronously
            await jobsDbContext.SaveChangesAsync();

            // Return the list of jobs that were added
            return jobs;
        }


        public Job GetJobById(int id)
        {
            var job = jobsDbContext.Jobs.Find(id);
            if (job == null)
                return null;
            return job;
        }

        public List<Job> GetJobs()
        {
            return jobsDbContext.Jobs.ToList();
        }

        public List<Job> GetJobsByDateAndUser(string user, DateTime date)
        {
            return jobsDbContext.Jobs
            .Where(j => j.IntegratorUser == user && j.StartTime.Date == date.Date)
            .ToList();
            
        }

        public List<Job> GetJobsByDateRange([FromQuery] string user, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            return jobsDbContext.Jobs
    .Where(j => j.IntegratorUser == user &&
                j.StartTime >= startDate &&
                j.StartTime < endDate)
    .ToList();

        }

        public List<Job> GetJobsByUser(string user)
        {
            return jobsDbContext.Jobs.Where(j => j.IntegratorUser == user).ToList();
        }
    }
}
