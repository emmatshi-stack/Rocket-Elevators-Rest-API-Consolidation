using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using buildingapi.Model;

namespace RocketElevatorsApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class addressesController : ControllerBase
    {
        private readonly emmanueltshibanguContext _context;

        public addressesController(emmanueltshibanguContext context)
        {
            _context = context;
        }

        // GET: api/interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Addresses>>> getAddresses()
        {
            return await _context.Addresses.ToListAsync();
        }

         // Action that recuperates a given intervention by Id 
        // GET: api/interventions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Addresses>> GetAddressesId(long id)
        {
            var inter = await _context.Addresses.FindAsync(id);

            if (inter == null)
            {
                return NotFound();
            }

            return inter;
        }

        [HttpGet("net/{id}")]
        public async Task<ActionResult<Addresses>> GetAddressesIdNet(long id)
        {
            var adr = await _context.Addresses.FindAsync(id);

            if (adr == null)
            {
                return NotFound();
            }

            return adr;
        }

        [HttpPut("met/{id}")]
        public async Task<IActionResult> PutmodifyAddresses(long id,string  Status,string City,string Country, string PostalCode, string SuiteApt, string TypeAddress,string NumberStreet  )
        {
            var adr = await _context.Addresses.FindAsync(id);
            if (adr == null)
            {
                return BadRequest();
            }


            adr.Status = Status;
            adr.City = City;
            adr.Country = Country;
            adr.PostalCode = PostalCode;
            adr.SuiteApt= SuiteApt;
            adr.TypeAddress= TypeAddress;
            adr.NumberStreet = NumberStreet;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressesExists(id))
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

        private bool AddressesExists(long id)
        {
            return _context.Elevators.Any(e => e.Id == id);
        }
       
        
        

      
    
    }
}