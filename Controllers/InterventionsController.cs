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
    public class interventionsController : ControllerBase
    {
        private readonly emmanueltshibanguContext _context;

        public interventionsController(emmanueltshibanguContext context)
        {
            _context = context;
        }

        // GET: api/interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interventions>>> Getinterventions()
        {
            return await _context.Interventions.ToListAsync();
        }

         // Action that recuperates a given intervention by Id 
        // GET: api/interventions/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Interventions>> GetInterventionById(long id)
        {
            var inter = await _context.Interventions.FindAsync(id);

            if (inter == null)
            {
                return NotFound();
            }

            return inter;
        }

       
        
        // Action that recuparates the status of the interventions being pending and not having a starting date
        // GET: api/interventions/getinterventionpeding
        [HttpGet("getinterventionpending")]
        public async Task<ActionResult<IEnumerable<Interventions>>> GetInterventionPending()
        {
            var service = await (from inter in _context.Interventions
                                  where inter.StartInterv == null && inter.Status == "Pending"
                                  select inter ).ToListAsync();

            if (service == null || !service.Any())
            {
                return NotFound();
            }

            return service;
        }

       // Action that modifies the status of the interventions as InProgress and ad the starting date of the intervention
        // PUT: api/interventions/id/updatestatuswithdatestart
        [HttpPut("{id}/updatestatuswithdatestart")]
        public async Task<IActionResult> PutmodifyInterventionStatuswithdateStart(long id)
        {
            var intervention = await _context.Interventions.FindAsync(id);
            
            intervention.Status = "InProgress";
            DateTime myvalue = DateTime.Now;
            string mytime = myvalue.ToString();
            intervention.StartInterv = mytime;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interventionsExists(id))
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


        // Action that modifies the status of the intervention as Completed and ad the ending date of the intervention
        // PUT: api/interventions/id/updatestatuswithdateend
        [HttpPut("{id}/updatestatuswithdateend")]
        public async Task<IActionResult> PutmodifyInterventionStatuswithdateEnd(long id)
        {
            var intervention = await _context.Interventions.FindAsync(id);

            intervention.Status = "Completed";
            DateTime firstTime = DateTime.Today.AddDays(-5);
            string SecondTime = firstTime.ToString();
            intervention.StopInterv = SecondTime;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!interventionsExists(id))
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

       

        
        private bool interventionsExists(long id)
        {
            return _context.Interventions.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> PostNewStudent([FromBody]Interventions student)
        {
            try{
                if (student == null)
                {
                    return BadRequest();
                };

                using (var ctx = new emmanueltshibanguContext())
                {
                    ctx.Interventions.Add(new Interventions()
                    {
                        Reports = student.Reports,
                        Author = student.Author,
                        CustomerId = student.CustomerId,
                        BuildingId = student.BuildingId,
                        BatteryId = student.BatteryId,
                        ColumnId = student.ColumnId,
                        ElevatorId = student.ElevatorId,
                    
                    });
                    ctx.SaveChanges();
                    await _context.SaveChangesAsync();
                    
                    
                }


            }
            catch(Exception e) {
                    int i =0;
            }
            return Ok();
        }

      
    
    }
}