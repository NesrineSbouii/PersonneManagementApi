using System;
using AutoMapper;
using PersonManagementApi.Data;
using PersonManagementApi.Entities;

namespace PersonManagementApi.Service
{
    public class JobService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public JobService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<JobDto> CreateJobAsync(JobDto jobtocreate)
        {
            var job = _mapper.Map<Job>(jobtocreate);
            _context.Emplois.Add(job);
            await _context.SaveChangesAsync();

            return _mapper.Map<JobDto>(job);
        }
    }
}
