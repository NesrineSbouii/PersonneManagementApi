using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonManagementApi.Data;
using PersonManagementApi.Entities;
using PersonManagementApi.Service;


namespace PersonManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PersonService _personService;
        private readonly JobService _jobService;

        public PersonController(DataContext context, PersonService personService, JobService jobService)
        {
            _context = context;
            _personService = personService;
            _jobService = jobService;
        }


        [HttpPost]
        public async Task<ActionResult<PersonDto>> CreatePerson([FromBody] PersonDto person)
        {
            var age = DateTime.Now.Year - person.BirthDate.Year;

            if (person == null)
            {
                return BadRequest("Person object is null");
            }
            if (age > 150)
            {
                return BadRequest("Personne trop âgée.");
            }

            await  _personService.CreatePersonAsync(person);
          
            return Ok(person);
        }


        [HttpPost("{personId}/emploi")]
        public async Task<ActionResult<JobDto>> AddJob(int personId, [FromBody] JobDto job)
        {
            var person = await _context.Personnes.FindAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            job.PersonneId = personId;
            await _jobService.CreateJobAsync(job);
         
            return Ok(job);
        }

        [HttpGet("Persons")]
        public async Task<ActionResult<List<PersonDto>>> GetAllPersons()
        {
            var result = await _personService.GetPersonsAsync(); 
            return Ok(result);
        }



        [HttpGet("company/{companyName}")]
        public async Task<ActionResult<List<PersonDto>>> GetPersonsByCompany(string campanyName)
        {
            var persons = await _personService.GetPersonsByCompanyNameAsync(campanyName);
            return Ok(persons);
        }

        [HttpGet("{personId}/jobs")]
        public async Task<IActionResult> GetJobsByDateRange(int personId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var jobs = await _context.Emplois
                .Where(j => j.PersonneId == personId && j.DateDebut >= startDate && j.DateDebut <= endDate)
                .ToListAsync();

            return Ok(jobs);
        }
    }
}

