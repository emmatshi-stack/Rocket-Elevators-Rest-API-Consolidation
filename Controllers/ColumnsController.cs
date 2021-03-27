using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buildingapi.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace buildingapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class columnsController : ControllerBase
    {
        private readonly emmanueltshibanguContext _context;

        public columnsController(emmanueltshibanguContext context)
        {
            _context = context;
        }


        // Retrieving of a list of columns
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Columns>>> Getcolumns()
        {
            return await _context.Columns.ToListAsync();
        }

        // Retrieving of a specific Column using the id
        [HttpGet("{id}")]
        public async Task<ActionResult<Columns>> Getcolumns(long id)
        {
            var column = await _context.Columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return column;
        }

       
        // Retrieving the current status of a specific Column
        [HttpGet("{id}/status")]
        public async Task<ActionResult<string>> GetcolumnStatus(long id)
        {
            var columns = await _context.Columns.FindAsync(id);

            if (columns == null)
            {
                return NotFound();
            }

            return columns.Status;
            
        }

        

        // setting the column status to a new one        
        [HttpPut("{id}/status")]
        public async Task<IActionResult> PutmodifyColumnStatus(long id, string Status)
        {
            if (Status == null)
            {
                return BadRequest();
            }

            var column = await _context.Columns.FindAsync(id);

            column.Status = Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!columnsExists(id))
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

        private bool columnsExists(long id)
        {
            return _context.Columns.Any(e => e.Id == id);
        }
    }
}