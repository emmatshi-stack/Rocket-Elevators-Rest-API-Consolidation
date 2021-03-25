using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using buildingapi.Model;

namespace buildingapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class elevatorsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public elevatorsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // Retrieving of a list of Elevators
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevators>>> Getelevators()
        {
            return await _context.Elevators.ToListAsync();
        }

        // Retrieving of a specific Elevators using the id
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevators>> GetelevatorsById(long id)
        {
            var elevators = await _context.Elevators.FindAsync(id);

            if (elevators == null)
            {
                return NotFound();
            }

            return elevators;
        }

       
        //Retrieving the current status of a specific Elevator
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetelevatorStatus(long id)
        {
            var elevator = await _context.Elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }
            
            return elevator.Status;
        }


        // Retrieving a list of Elevators that are not in operation at the time of the request
        [HttpGet("inactive")]
        public async Task<ActionResult<List<Elevators>>> GetinactiveElevators()
        {
            var elevator = await _context.Elevators
            .Where(e => e.Status != "Online").ToListAsync();
                

            if (elevator == null)
            {
                return NotFound();
            }

            return elevator;
        }


       
        //Changing the status of a specific Elevator       
        [HttpPut("{id}/setStatus")]
        public async Task<IActionResult> PutmodifyElevatorStatus(long id, string status)
        {
            if (status == null)
            {
                return BadRequest();
            }

            var elevator = await _context.Elevators.FindAsync(id);

            elevator.Status = status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!elevatorsExists(id))
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

        private bool elevatorsExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }
    }
}