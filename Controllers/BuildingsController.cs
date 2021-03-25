using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using buildingapi.Model;


namespace buildingapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class buildingsController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public buildingsController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // getting the list of all buildings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Buildings>>> Getbuildings()
        {
            return await _context.Buildings.ToListAsync();
        }
        
        
        // 
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetbuildingList()
        {
         
            
             var building = await (from cust in _context.Buildings
                            join bat in _context.Batteries on cust.Id equals bat.BuildingId
                            join col in _context.Columns on bat.Id equals col.BatteryId
                            join ele in _context.Elevators on col.Id equals ele.ColumnId
                            join adr in _context.Addresses on cust.AddressId equals adr.Id
                            where ele.Status == "Offline" || col.Status == "Offline" || bat.Status == "Offline"
                            select cust).Distinct().ToListAsync();
                  
            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

        [HttpGet("gh")]
        public async Task<ActionResult<IEnumerable<Buildings>>> GetbuildingListh()
        {
         
            
             var building = await (from cust in _context.Buildings
                            join bat in _context.Batteries on cust.Id equals bat.BuildingId
                            join col in _context.Columns on bat.Id equals col.BatteryId
                            join ele in _context.Elevators on col.Id equals ele.ColumnId
                            where ele.Status == "Online" || col.Status == "Online" || bat.Status == "Online"
                            select cust).Distinct().ToListAsync();
                  
            if (building == null)
            {
                return NotFound();
            }

            return building;
        }
        
    }

}









