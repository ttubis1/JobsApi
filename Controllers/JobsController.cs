using AutoMapper;
using JobsApi.Data;
using JobsApi.Data.Repositories;
using JobsApi.Models;
using JobsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        readonly JobService _service;
        readonly IMapper _mapper;
        public JobsController(JobService service,IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Job>>> GetAllJobs()
        {
            var retjobs = _service.GetAllJobs();
            return Ok(retjobs);
        }

        [HttpGet("user/{user}")]
        //[Authorize] // Require authentication
        public async Task<ActionResult<List<Job>>> GetJobsByUser(string user)
        {
            // Get the currently authenticated username
            var currentUser = User.Identity?.Name;

            // Return jobs where IntegratorUser matches the user
            var jobs = _service.GetAllJobs()
                .Where(j => j.IntegratorUser == user)
                .ToList();

            return Ok(jobs);
        }

        [HttpGet("bydate/{user}/{date}")]
        //[Authorize]
        public async Task<ActionResult<List<Job>>> GetJobsByDate(string user, DateTime date)
        {
            // Get the currently authenticated user
            var jobs = _service.GetJobsByDateAndUser(user,date);
            return Ok(jobs);
            
        }

        [HttpGet("range")]
        public async Task<ActionResult<List<Job>>> GetJobsByDateRange(
    [FromQuery] string user,
    [FromQuery] DateTime startDate,
    [FromQuery] DateTime endDate)
        {
            // Normalize to full-day boundaries
            var startOfDay = startDate.Date;
            var endOfDay = endDate.Date.AddDays(1); // exclusive
            
            {
                var jobs = _service.GetAllJobs()
                    .Where(j => j.IntegratorUser == user &&
                                j.StartTime >= startOfDay &&
                                j.StartTime < endOfDay)
                    .ToList();

                return Ok(jobs);
            }
        }
        [HttpGet("machine/")]
        //[Authorize]
        public async Task<ActionResult<List<Job>>> GetJobsByMachine([FromQuery] string user, [FromQuery] string machine)
        {
            var jobs = _service.GetAllJobs()
                .Where(j => j.MachineName == machine)
                .ToList();

            return Ok(jobs);
        }
        [HttpPost("Create")]
        //[Authorize(Roles ="Admin")]
        public Job AddJob([FromBody] Job dto)
        {
            return _service.AddJob(dto);
        }
        [HttpPost("CreateMul")]
        //[Authorize(Roles ="Admin")]
        public IActionResult AddJobs([FromBody] List<Job> dto)
        {
            foreach (var job in dto)
            {  _service.AddJob(job); }
            return Ok(true);
        }
        [HttpPost("duplicate")]
        public async Task<ActionResult<List<Job>>> DuplicateUserRows([FromQuery] string? user)
        {
            var jobs = string.IsNullOrEmpty(user)
        ? _service.GetAllJobs()              // fallback if no user provided
        : _service.GetJobByUser(user);

            var dupJobs = jobs.Select(j => new Job
             {
                 JobId = 0,
                 JobName = j.JobName,
                 Copies = j.Copies,
                 Printed = j.Printed,
                 Width = j.Width,
                 Height = j.Height,
                 IntegratorUser = j.IntegratorUser,
                 MachineName = j.MachineName,
                 JobStatus = j.JobStatus,
                 StartTime = j.StartTime,
                 EndTime = j.EndTime
             })
                   .ToList();

            await _service.AddJobs(dupJobs);
            return Ok(dupJobs);
        }
    }
}
