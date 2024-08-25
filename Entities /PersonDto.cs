using System;
using PersonManagementApi.Data;

namespace PersonManagementApi.Entities
{
	public class PersonDto
	{
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<JobDto>? Jobs { get; set; }
    }
}

