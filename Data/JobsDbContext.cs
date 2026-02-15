using JobsApi.Definitions;
using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Data
{
    public class JobsDbContext : DbContext , IJobsDbContext

    {
        public JobsDbContext(DbContextOptions<JobsDbContext> options)
        : base(options)
        {
           
        }

        // Example DbSets
        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = 1,
                    JobName = "Poster Printing2",
                    Copies = 100,
                    Printed = 100,
                    Width = 50,
                    Height = 70,
                    IntegratorUser = "alice",
                    MachineName = "LPT2",
                    JobStatus = Definitions.JobStatus.Ended,
                    StartTime = new DateTime(2025, 11, 19, 9, 0, 0),
                    EndTime = new DateTime(2025, 11, 19, 12, 0, 0)
                },
                new Job
                {
                    JobId = 2,
                    JobName = "Flyer Distribution3",
                    Copies = 500,
                    Printed = 250,
                    Width = 21,
                    Height = 29,
                    IntegratorUser = "bob",
                    MachineName = "LPT3",
                    JobStatus = Definitions.JobStatus.Failed,
                    StartTime = new DateTime(2025, 11, 20, 10, 0, 0),
                    EndTime = new DateTime(2025, 11, 20, 15, 0, 0)
                },
                new Job
                {
                    JobId = 3,
                    JobName = "Business Cards3",
                    Copies = 200,
                    Printed = 12,
                    Width = 9,
                    Height = 5,
                    IntegratorUser = "charlie",
                    MachineName = "LPT2",
                    JobStatus = Definitions.JobStatus.Started,
                    StartTime = new DateTime(2025, 11, 21, 8, 30, 0),
                    EndTime = new DateTime(2025, 11, 21, 11, 0, 0)
                },
                // Jobs executed 2 days ago (17 Nov 2025)
                new Job
                {
                    JobId = 4,
                    JobName = "Brochure Print3",
                    Copies = 150,
                    Printed = 70,
                    Width = 21,
                    Height = 29,
                    IntegratorUser = "alice",
                    MachineName = "LPT3",
                    JobStatus = Definitions.JobStatus.Started,
                    StartTime = new DateTime(2025, 11, 17, 9, 0, 0),
                    EndTime = new DateTime(2025, 11, 17, 10, 0, 0)
                },
                new Job
                {
                    JobId = 5,
                    JobName = "Poster Batch4",
                    Copies = 20,
                    Printed = 20,
                    Width = 60,
                    Height = 90,
                    IntegratorUser = "bob",
                    MachineName = "LPT1",
                    JobStatus = Definitions.JobStatus.Ended,
                    StartTime = new DateTime(2025, 11, 17, 11, 0, 0),
                    EndTime = new DateTime(2025, 11, 17, 12, 0, 0)
                },
                new Job
                {
                    JobId = 6,
                    JobName = "Sticker Run4",
                    Copies = 1000,
                    Printed = 700,
                    Width = 5,
                    Height = 5,
                    IntegratorUser = "charlie",
                    MachineName = "LPT1",
                    JobStatus = Definitions.JobStatus.Started,
                    StartTime = new DateTime(2025, 11, 17, 14, 0, 0),
                    EndTime = new DateTime(2025, 11, 17, 15, 0, 0)
                }
            );
        }

        public List<Job> GetJobs()
        {
            return Jobs.ToList();
        }
    }
}
