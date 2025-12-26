using HRMS_2.DBcontexts;
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
          private readonly HRMS_2Context _dbcontext;

        public EmployeesController(HRMS_2Context dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("GetByCriteria")]
        public  IActionResult GetByCriteria([FromQuery]SearchEmployeeDto searchemp) {
            var result = from emp in _dbcontext.Employees
                         from dep in _dbcontext.Departments.Where(x => x.Id == emp.DepartmentId).DefaultIfEmpty()
                         from man in _dbcontext.Employees.Where(x=>x.Id == emp.ManagerId).DefaultIfEmpty()
                         from Lookup in _dbcontext.Lookups.Where(x=>x.Id == emp.PositionId)
                         where
                         (searchemp.PositionId == null || emp.PositionId==searchemp.PositionId)&&
                         (searchemp.Name == null|| emp.FirstName.ToUpper().Contains(searchemp.Name.ToUpper()))
                         orderby emp.Id descending
                         select new EmployeeDto {
                             Id = emp.Id,
                             Name = emp.FirstName + " " + emp.LastName,
                             Email = emp.Email,
                             PositionId = emp.PositionId,
                             PositionName=Lookup.Name,
                             BirthDay = emp.BirthDate,
                             Salary= emp.Salary,
                             DepartmentId = emp.DepartmentId,
                             DepartmentName = dep.Name,
                             ManagerName=man.FirstName,
                             ManagerId = emp.ManagerId,


                         };
            return Ok(result);

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(long id)
        {
            if (id == 0) {
            return BadRequest("Id value is not valid");
            };

            var result = _dbcontext.Employees.Select( x => new EmployeeDto {
                Id = x.Id,
                Name = x.FirstName + " " + x.LastName,
                Email = x.Email,
                PositionId = x.PositionId,
                PositionName=x.Lookup.Name,
                BirthDay = x.BirthDate,
                Salary = x.Salary,
                DepartmentId = x.DepartmentId,
                DepartmentName= x.Department.Name,
                ManagerId= x.ManagerId,
                ManagerName=x.Manager.FirstName,
            }).FirstOrDefault(x => x.Id == id);
            if (result == null) {
                return NotFound("Employee Not Found");
            }
                return Ok(result);
        }
        [HttpPost("Add")]
        public  IActionResult Add( [FromBody] SaveEmployeeDto employeeDto) {
            var employee = new Employee()
            {
                Id = 0,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                PositionId = employeeDto.PositionId,
                BirthDate=employeeDto.BirthDay,
                Salary = employeeDto.Salary,
                DepartmentId = employeeDto.DepartmentId,
                ManagerId = employeeDto.ManagerId,

            };
            _dbcontext.Employees.Add(employee);
            _dbcontext.SaveChanges();
            return Ok(employee);
        }
        [HttpPut("Update")]
        public IActionResult Update([FromBody] SaveEmployeeDto employeeDto) {
            var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == employeeDto.Id);
            if (employee == null) {
                return NotFound("employee not found");
            }
            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.PositionId= employeeDto.PositionId;
            employee.BirthDate = employeeDto.BirthDay;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.ManagerId = employeeDto.ManagerId;
            _dbcontext.SaveChanges();
            return Ok(employee);

            


        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromQuery] long id)
        {
            var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
            {
                return NotFound("employee not found");
            }
            _dbcontext.Employees.Remove(employee);
            _dbcontext.SaveChanges();
            return Ok(employee);

        }
    }
    
}
