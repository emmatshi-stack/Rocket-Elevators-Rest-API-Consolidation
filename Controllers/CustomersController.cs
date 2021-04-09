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

        // [HttpGet("{email}")]
        // public async Task<ActionResult<IEnumerable<Customers>>> GetbatteriesByEmail(string email)
        // {
        //     var emailREturn = await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync();
           
        //     return  await _context.Customers.Where(b => b.CpyContactEmail == email).ToListAsync();
            
        // }

        
        
        
        
         
    }
}
