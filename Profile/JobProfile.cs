using AutoMapper;
using JobsApi.Models;


namespace JobsApi.Profile
{
    public class JobProfile : AutoMapper.Profile
    {
        public JobProfile()
        {
            // DTO → Entity
            CreateMap<JobDto, Job>()
                .ForMember(dest => dest.JobId, opt => opt.Ignore()) // DB generates ID
                .ForMember(dest => dest.Printed, opt => opt.MapFrom(src => 0)) // initialize
                .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => DateTime.MinValue)) // set current time
                .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => DateTime.MinValue)); // default

            // Entity → DTO
            CreateMap<Job, JobDto>();
        }

    }
}
