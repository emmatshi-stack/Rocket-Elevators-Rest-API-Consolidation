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
    public class employeesController : ControllerBase
    {
        private readonly emmanueltshibanguContext _context;

        public employeesController(emmanueltshibanguContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> GetEmployee()
        {
            return await _context.Employees.ToListAsync();;
        }

        [HttpGet("{email}/{pwd}")]
        public async Task<ActionResult<Employees>> GetEmployeesByEmail(string email, string pwd)
        {
            List<Employees> employees = await _context.Employees.Where(b => b.Email == email && b.User.EncryptedPassword == pwd).ToListAsync();
            if (employees == null || employees.Count() == 0)
            {
                return await Task.FromResult(new Employees());
            }
            else{
                return  await Task.FromResult(employees.First());
            }
           
            
        }

        // Retrieving of a list of Elevators
        

        // Retrieving of a specific Elevators using the id
        
       
        //Retrieving the current status of a specific Elevator
       


        
    }
}