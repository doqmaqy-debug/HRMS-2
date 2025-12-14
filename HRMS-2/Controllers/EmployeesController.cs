using HRMS_2.Dtos;
using HRMS_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        public static List<Employee> employees = new List<Employee>() {
        new Employee(){Id=1,FirstName = "yaser",LastName="doqmaq",Email="123myEmail.com", Position= "Manager",BirthDay=new DateTime(2000,6,27)},
        new Employee(){Id=2,FirstName = "Ahmad",LastName="doqmaq" , Position= "HR",BirthDay=new DateTime(2001,2,23)},
       new Employee(){Id=3,FirstName = "omar",LastName="doqmaq" , Position= "Developer",BirthDay=new DateTime(1999,3,13)},
        new Employee(){Id=4,FirstName = "Ali",LastName="doqmaq" , Email="123myEmail.com",Position= "Developer", BirthDay = new DateTime(2005,6,14)},

        };

        [HttpGet("GetByCriteria")]
        public  IActionResult GetByCriteria([FromQuery]SearchEmployeeDto searchemp) {
            var result = from emp in employees
                         where (searchemp.Position == null || emp.Position.ToUpper().Contains(searchemp.Position.ToUpper()))
                         &&(searchemp.Name == null|| emp.FirstName.ToUpper().Contains(searchemp.Name.ToUpper()))
                         orderby emp.Id descending
                         select new EmployeeDto {
                             Id = emp.Id,
                             Name = emp.FirstName + " " + emp.LastName,
                             Email = emp.Email,
                             Position = emp.Position,
                             BirthDay = emp.BirthDay,


                         };
            return Ok(result);

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromQuery ]long id)
        {
            if (id == 0) {
            return BadRequest("Id value is not valid");
            };

            var result = employees.Select( x => new EmployeeDto {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                Email = x.Email,
                Position = x.Position,
                BirthDay = x.BirthDay,
            }).FirstOrDefault(x => x.Id == id);
            if (result == null) {
                return NotFound("Employee Not Found");
            }
                return Ok(result);
        }
        [HttpPost("Add")]
        public  IActionResult Add( [FromBody] SaveEmployeeDto employeeDto) {
            var employee= new Employee() {
              Id= (employees.LastOrDefault()?.Id??0)+1,
              FirstName= employeeDto.FirstName,
              LastName= employeeDto.LastName,
              Email=employeeDto.Email,
              Position= employeeDto.Position,
            };
            employees.Add(employee);
            return Ok(employee);
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] SaveEmployeeDto employeeDto) {
            var employee = employees.FirstOrDefault(x => x.Id == employeeDto.Id);
            if (employee == null) {
                return NotFound("employee not found");
            }
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.Position= employeeDto.Position;
            employee.BirthDay = employeeDto.BirthDay;
            return Ok(employee);

            


        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromQuery] long id)
        {
            var employee = employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound("employee not found");
            }
            employees.Remove(employee);
            return Ok(employee);

        }
    }
    
}
