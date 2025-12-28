using HRMS_2.DBcontexts;
using HRMS_2.Dtos;
using HRMS_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRMS_2.Controllers
{
    [Authorize]
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
            try {

                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;   
                
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
                             UserId = emp.UserId,


                         };
                if (role?.ToUpper() != "HR" && role?.ToUpper() !="Admin")
                {
                    result = result.Where(x => x.UserId == long.Parse( userId) );
                }

                return Ok(result);
            }
            
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest("Id value is not valid");
                }
                ;

                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = _dbcontext.Employees.Select(x => new EmployeeDto
                {
                    Id = x.Id,
                    Name = x.FirstName + " " + x.LastName,
                    Email = x.Email,
                    PositionId = x.PositionId,
                    PositionName = x.Lookup.Name,
                    BirthDay = x.BirthDate,
                    Salary = x.Salary,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department.Name,
                    ManagerId = x.ManagerId,
                    ManagerName = x.Manager.FirstName,
                    UserId=x.UserId,
                }).FirstOrDefault(x => x.Id == id);
                if (result == null)
                {
                    return NotFound("Employee Not Found");
                }
                if (role?.ToUpper() != "HR" && role?.ToUpper() != "Admin")  
                {
                    if (result.UserId != long.Parse(userId))
                        {
                        return Forbid();   
                        }
                }   

                    return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);            
            }
        }
        [Authorize(Roles ="HR,Admin")]
        [HttpPost("Add")]
        public  IActionResult Add( [FromBody] SaveEmployeeDto employeeDto) {
            try
            {
                var user = new User()
                {
                    Id = 0,
                    UserName = $"{employeeDto.FirstName}_{employeeDto.LastName}_HRMS",
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword($"{employeeDto.FirstName}@123"),
                    IsAdmid = false
                };

                var IsUserName = _dbcontext.Users.Any(x => x.UserName.ToUpper() == user.UserName.ToUpper());
                if (IsUserName) 
                {
                    return BadRequest("UserName Is Already Exist , Please Choose Another One ");
                }
                _dbcontext.Users.Add(user); 

                var employee = new Employee()
                {
                    Id = 0,
                    FirstName = employeeDto.FirstName,
                    LastName = employeeDto.LastName,
                    Email = employeeDto.Email,
                    PositionId = employeeDto.PositionId,
                    BirthDate = employeeDto.BirthDay,
                    Salary = employeeDto.Salary,
                    DepartmentId = employeeDto.DepartmentId,
                    ManagerId = employeeDto.ManagerId,
                    User=user,

                };
                _dbcontext.Employees.Add(employee);
                _dbcontext.SaveChanges();
                return Ok(employee);
            }
            catch (Exception ex) 
            {   
                return BadRequest(ex.Message);
            }
        
        }
        [Authorize(Roles = "HR,Admin")]
        [HttpPut("Update")]
        public IActionResult Update([FromBody] SaveEmployeeDto employeeDto) {
            var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == employeeDto.Id);

            if (employee == null) {
                return NotFound("employee not found");
            }
            try {employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.PositionId= employeeDto.PositionId;
            employee.BirthDate = employeeDto.BirthDay;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.ManagerId = employeeDto.ManagerId;
            _dbcontext.SaveChanges();
            return Ok(employee);
            }
            catch(NullReferenceException ex)
            {
                return NotFound("Employee Does Not Exist");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            };

            

            


        }
        [Authorize(Roles = "HR,Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromQuery] long id)
        {
            try {
                var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound("employee not found");
                }
                _dbcontext.Employees.Remove(employee);
                _dbcontext.SaveChanges();
                return Ok(employee);
            }
            catch (Exception ex) 
            {   
                return BadRequest(ex.Message);
            };

        }
    }
    
}
