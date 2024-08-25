using System;
using System.Collections.ObjectModel;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonManagementApi.Data;
using PersonManagementApi.Entities;

namespace PersonManagementApi.Service
{
	public class PersonService
	{

        private readonly DataContext _context;
        private readonly IMapper _mapper;
      
        public PersonService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
           

        }

        public async Task<PersonDto> CreatePersonAsync(PersonDto personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            _context.Personnes.Add(person);
            await _context.SaveChangesAsync();
           
            return _mapper.Map<PersonDto>(person);
        }


        public async Task<List<PersonDto>> GetPersonsAsync()
        {
            var persons = await _context.Personnes
               .Include(p => p.Jobs)
               .OrderBy(p => p.FirstName)
               .ThenBy(p => p.LastName) 
               .ToListAsync();

            var personList = _mapper.Map<List<PersonDto>>(persons);

            foreach (var person in personList)
            {
                person.Jobs = person?.Jobs?.Where(j => j.DateFin == null).ToList();
            }

            return personList;
        }


        public async Task<List<PersonDto>> GetPersonsByCompanyNameAsync(string companyName)
        {
            
            var persons = await _context.Personnes
                .Include(p => p.Jobs)
                .Where(p => p.Jobs.Any(j => j.NomEntreprise == companyName))
                .Distinct()
                .ToListAsync();

            
            var personList = _mapper.Map<List<PersonDto>>(persons);


            return personList;
        }
    }
}

