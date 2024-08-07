using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonManagementApi.Data;
using PersonManagementApi.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonManagementApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly DataContext _context;

        public PersonController(DataContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<Personne>> CreatePerson([FromBody] Personne person)
        {
            var age = DateTime.Now.Year - person.DateNaissance.Year;
            if (age > 150)
            {
                return BadRequest("Personne trop âgée.");
            }

            _context.Personnes.Add(person);
            await _context.SaveChangesAsync();
            return Ok(person);
        }


        [HttpPost("{personId}/emploi")]
        public async Task<ActionResult<Emploi>> AddJob(int personId, [FromBody] Emploi emploi)
        {
            var person = await _context.Personnes.FindAsync(personId);
            if (person == null)
            {
                return NotFound();
            }

            emploi.PersonneId = personId;
            _context.Emplois.Add(emploi);
            await _context.SaveChangesAsync();
            return Ok(emploi);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _context.Personnes
                .Include(p => p.Emplois)
                .OrderBy(p => p.Nom)
                .ThenBy(p => p.Prenom)
                .ToListAsync();

            var result = persons.Select(p => new
            {
                p.Id,
                p.Prenom,
                p.Nom,
                p.DateNaissance,
                Age = DateTime.Now.Year - p.DateNaissance.Year,
                CurrentJobs = p.Emplois.Where(j => j.DateFin == null)
            });

            return Ok(result);
        }

        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetPersonsByCompany(string nomEntreprise)
        {
            var persons = await _context.Emplois
                .Where(j => j.NomEntreprise == nomEntreprise)
                .Select(j => j.Personne)
                .Distinct()
                .ToListAsync();

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

