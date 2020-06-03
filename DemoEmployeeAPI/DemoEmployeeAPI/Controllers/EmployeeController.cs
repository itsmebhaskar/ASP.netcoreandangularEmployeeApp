using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoEmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoEmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeContext _employeeContext;
        public EmployeeController(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        

        [HttpGet]
        public IActionResult GetEmployees()
        {
          var employees = _employeeContext.Employee.ToList();
          return  Ok(employees);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateEmployee([FromBody]Employee employee)
        {
            _employeeContext.Employee.Update(employee);
             return  Ok(_employeeContext.SaveChanges());
        }


        [HttpPost]
        
        public IActionResult AddEmployee(Employee employee)
        {
            _employeeContext.Employee.Add(employee);
            return Ok(_employeeContext.SaveChanges());
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            Employee employee = new Employee() { Id=id };
            _employeeContext.Employee.Attach(employee);
            _employeeContext.Employee.Remove(employee);
            return Ok(_employeeContext.SaveChanges());
        }



    }


   
}