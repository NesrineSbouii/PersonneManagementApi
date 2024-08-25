using System;
using AutoMapper;
using PersonManagementApi.Data;
using PersonManagementApi.Entities;

namespace PersonManagementApi.Mapping
{
	public class JobProfile: Profile
    {
        public JobProfile()
        {
            CreateMap<JobDto, Job>()
              .ReverseMap();
        }
    }

}

