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
        private readonly emmanueltshibanguContext _context;

        public BatteriesController(emmanueltshibanguContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Batteries>>> GetgBatteries(long cid)
        
        {
            return await _context.Batteries.ToListAsync();
            
        
        }

        // getting the list of all batteries
        [HttpGet("{cid}")]
        public async Task<ActionResult<IEnumerable<Batteries>>> GetBatteries(long cid)
        
        {
            
            var batt = await _context.Batteries.Where(bat => bat.BuildingId == cid).ToListAsync();
            if (batt == null)
            {
                return NotFound();
            }

            return batt;
        }

        // getting the list of all batteries
        [HttpGet("bat/{cid}")]
        public async Task<ActionResult<List<Batteries>>> GetBatteriesProduct(long cid)
        
        {
            var build = await _context.Buildings.FindAsync(cid);
            Console.WriteLine("3333333333333333333333333333333333333333333333333333333333333");
            var batt = await _context.Batteries.Where(bat => bat.BuildingId == build.Id).ToListAsync();
           List<Batteries> liste = new List<Batteries>();
            foreach(Batteries i in batt){
                   var list= new Batteries{Id = i.Id
                   };
                   liste.Add(list);
            }
         //  List<Batteries> liste = new List<Batteries>{new Batteries{Id = batt.FirstOrDefault().Id,}};
            if (batt == null)
            {
                return NotFound();
            }
           // var listBulding = (from i in batt select i.Building).ToList();
          //  var listBatt= from i in listBulding select i.Batteries;
            return liste;
        }

        //Getting the status of a particular battery 
        // [HttpGet("{id}/status")]
        // public async Task<ActionResult<string>> GetbatteryStatus(long Id)
        // {
        //     var bat = await _context.Batteries.FindAsync(Id);

        //     if (bat == null)
        //     {
        //         return NotFound();
        //     }

        //     return bat;

        // }
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
            }return NoContent();
        }
        private bool BatteriesExists(long id)
        {
            return _context.Batteries.Any(e => e.Id == id);
        }       
        
        
         
    }
}
