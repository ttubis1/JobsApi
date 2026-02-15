using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Data
{
    public interface IJobsDbContext
    {
        DbSet<Job> Jobs { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }



}
