﻿
namespace PersonManagementApi.Data
{
	public class Person
	{
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}

