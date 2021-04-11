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
    public class CustomersController : ControllerBase
    {
        private readonly emmanueltshibanguContext _context;

        public CustomersController(emmanueltshibanguContext context)
        {
            _context = context;
        }

        // getting the list of all batteries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> Getbatteries()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<Customers>>> GetbatteriesByEmail(string email)
        {
            if (await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync() == null)
            {
                return NotFound();
            }
            else{
                return  await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync();
            }
           
            
        }

        [HttpPost("update")]
        public async Task<IActionResult> PutmodifyCustomer(Customers cust)
        {
            
            if (cust == null)
            {
                return BadRequest();
            }
            var cus = await _context.Customers.FindAsync(cust.Id);

            cus.CpyContactFullName = cust.CpyContactFullName;
            cus.CpyContactPhone = cust.CpyContactPhone;
            cus.CpyContactEmail = cust.CpyContactEmail;
            cus.TechManagerServiceEmail = cust.TechManagerServiceEmail;
           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(cust.Id))
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

        private bool CustomersExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        // [HttpGet("{email}")]
        // public async Task<ActionResult<IEnumerable<Customers>>> GetbatteriesByEmail(string email)
        // {
        //     var emailREturn = await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync();
           
        //     return  await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync();
            
        // }

        
        
        
        
         
    }
}
