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
    public class DepartementController : ControllerBase
    {

        private readonly AuthenticationContext _context;

        public DepartementController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/<DepartementController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departement>>> GetAllDepartements()
        {
            return await _context.Departements
                .ToListAsync();

        }

        // GET api/<DepartementController>/5
        [HttpGet("{id}")]
        public async Task<Departement> GetDepartement(Guid id)
        {

            return await _context.Departements.FindAsync(id);

        }

        // POST api/<DepartementController>
        [HttpPost]
        public async Task AddDepartement(Departement departement)
        {


            await _context.Departements.AddAsync(departement);
            await _context.SaveChangesAsync();

        }

        // PUT api/<DepartementController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartement(Guid id, Departement departement)
        {
            if (id != departement.DepartementID)
            {
                return BadRequest();
            }

            _context.Entry(departement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartementExists(id))
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

        // DELETE api/<DepartementController>/5
        [HttpDelete("{id}")]
        public async Task DeleteDepartement(Guid id)
        {


            var departement = await _context.Departements.FindAsync(id);
            if (departement == null)
                return;

            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();

        }

        private bool DepartementExists(Guid id)
        {
            return _context.Departements.Any(e => e.DepartementID == id);
        }
    }
}
