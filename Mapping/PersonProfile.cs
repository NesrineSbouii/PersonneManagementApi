
using PersonManagementApi.Entities;
using AutoMapper;

using PersonManagementApi.Data;

namespace PersonManagementApi.Mapping
{
	public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>()
              .ReverseMap();
        }
    }
	
}

