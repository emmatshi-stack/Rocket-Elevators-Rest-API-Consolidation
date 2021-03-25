using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using buildingapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace buildingapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BatteriesController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public BatteriesController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // getting the list of all batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batteries>>> Getbatteries()
        {
            return await _context.Batteries.ToListAsync();
        }

        // Getting the status of a particular battery 
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetbatteryStatus(long Id)
        {
            var bat = await _context.Batteries.FindAsync(Id);

            if (bat == null)
            {
                return NotFound();
            }

            return bat.Status;

        }
        // PUT: upadating the status of a particular battery
        [HttpPut("{id}/updatestatus")]
        public async Task<IActionResult> PutmodifBatterySatus(long Id, string Status)
        {                       
            if (Status == null)
            {
                return BadRequest();
            }
            var battery = await _context.Batteries.FindAsync(Id);
            battery.Status = Status;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BatteriesExists(Id))
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
        private bool BatteriesExists(long id)
        {
            return _context.Batteries.Any(e => e.Id == id);
        }


       
        // Getting a particular battery using his Id
        [HttpGet("{id}")]
        public async Task<ActionResult<Batteries>> Getbatteries(long Id)
        {
            var bat = await _context.Batteries.FindAsync(Id);

            if (bat == null)
            {
                return NotFound();
            }

            return bat;
        }
        
        
        
        
         
    }
}
