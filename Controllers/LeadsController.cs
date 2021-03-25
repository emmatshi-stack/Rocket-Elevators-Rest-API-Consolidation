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
    public class LeadsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public LeadsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leads>>> Getleads()
        {

            return await _context.Leads.ToListAsync();
        }


        
        [HttpGet("{leadsNoCustomer}")]
        public async Task<ActionResult<List<Leads>>> GetleadsCustomers()
        {
            var leads = await _context.Leads.Where(l => l.CustomerId == null).ToListAsync();
            var newLeads = leads.Where(e => e.created_at >= DateTime.Today.AddDays(-30)).ToList();

            if (newLeads == null)
            {
                return NotFound();
            }

            return newLeads;
        }
    }
}
