using JobsApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Reflection.Metadata.BlobBuilder;

namespace JobsApi.Data.Repositories
{
    public class JobsRepositoryMem : IJobRepository
    {
        List<Job> jobs = new List<Job>()
            {
                new Job
                {

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
            };

        public Job AddJob([FromBody] Job job)
        {
            jobs.Add(job);
            return job;
        }
        private static readonly List<Job> _jobs = new();
        private static int _nextId = 1;
        private static readonly object _lock = new();

        public Task<List<Job>> AddJobsAsync(List<Job> jobs)
        {
            lock (_lock)
            {
                foreach (var job in jobs)
                {
                    // Assign IDs if not set
                    if (job.JobId == 0)
                    {
                        job.JobId = _nextId++;
                    }

                    _jobs.Add(job);
                }
            }

            // Return the added jobs
            return Task.FromResult(jobs);
        }


        public Job GetJobById(int id)
        {
            return jobs[id];
        }

        public List<Job> GetJobs()
        {
            return jobs;
        }

        public List<Job> GetJobsByDateAndUser(string currentUser, DateTime date)
        {
            
            // Define start and end of the requested date
            var startOfDay = date.Date;
            var endOfDay = date.Date.AddDays(1);

            // Query jobs for that user on the specific date

            var filjobs = jobs.Where(j => j.IntegratorUser == currentUser &&
                            j.StartTime >= startOfDay &&
                            j.StartTime < endOfDay)
                .ToList();
            return filjobs;
        }

        public List<Job> GetJobsByDateRange([FromQuery] string user, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var filjobs = jobs.Where(j => j.IntegratorUser == user &&
                            j.StartTime >= startDate &&
                            j.EndTime < endDate)
                .ToList();
            return filjobs;
        }

        public List<Job> GetJobsByUser(string user)
        {
            var filjobs = jobs.Where(j => j.IntegratorUser == user )
               .ToList();
            return filjobs;
        }
    }
}
