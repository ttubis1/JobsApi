using JobsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Data
{
    public class SqliteAppDbContext : DbContext , IJobsDbContext

    {
        public SqliteAppDbContext(DbContextOptions<SqliteAppDbContext> options)
            : base(options) { }

        public DbSet<Job> Jobs { get; set; }
    }

}
