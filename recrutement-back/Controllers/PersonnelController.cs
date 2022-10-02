using AppRecrutement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppRecrutement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {

        private readonly AuthenticationContext _context;

        public PersonnelController(AuthenticationContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("stats")]
        public async Task<Object> getstats()
        {

            List<Departement> listDep = _context.Departements.Include(p => p.Personnels).ToList();

            List<Personnel> listperso = _context.Personnel.Include(p => p.departement).ToList();


            List<stats> stats = new List<stats>();


            var s = _context.Personnel.Count();

            foreach (Departement d in listDep)
            {
                var t = _context.Personnel
                .Include(p => p.departement)
                .Where(p => p.DepartementId == d.DepartementID)
                .Count();

                Console.WriteLine(d);
                stats.Add(new stats() { name = d.Libelle, value = t }

                );
            }

            return stats;


        }
        // GET: api/<PersonnelController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetAllPersonnels()
        {
            return await _context.Personnel.Include(p => p.departement)
                .ToListAsync();

        }

        // GET: api/<PersonnelController>
        [HttpGet]
        [Route("getPersonnelByDepartement")]
        public async Task<ActionResult<IEnumerable<Personnel>>> GetPersonnelsByDepartement(Guid id)
        {
            return await _context.Personnel
                .Include(p => p.departement)
                .Where(p=>p.DepartementId == id)
                .ToListAsync();

        }

        // GET api/<PersonnelController>/5
        [HttpGet("{id}")]
        public async Task<Personnel> GetPersonnel(Guid id)
        {

            return await _context.Personnel.FindAsync(id);

        }

        // POST api/<PersonnelController>
        [HttpPost]
        public async Task AddPersonnel(Personnel personnel)
        {


            await _context.Personnel.AddAsync(personnel);
            await _context.SaveChangesAsync();

        }

        // PUT api/<PersonnelController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonnel(Guid id, Personnel personnel)
        {
            if (id != personnel.idPersonnel)
            {
                return BadRequest();
            }

            _context.Entry(personnel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonnelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE api/<PersonnelController>/5
        [HttpDelete("{id}")]
        public async Task DeletePersonnel(Guid id)
        {


            var personnel = await _context.Personnel.FindAsync(id);
            if (personnel == null)
                return;

            _context.Personnel.Remove(personnel);
            await _context.SaveChangesAsync();

        }

        private bool PersonnelExists(Guid id)
        {
            return _context.Personnel.Any(e => e.idPersonnel == id);
        }
    }
}
