using HRMS_2.DBcontexts;
using HRMS_2.Dtos;
using HRMS_2.Dtos.Shared;
using HRMS_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HRMS_2.Enums.Lookups_enums;

namespace HRMS_2.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly HRMS_2Context _dbcontext;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        public EmployeesController(HRMS_2Context dbcontext, IWebHostEnvironment env,IConfiguration config )
        {
            _dbcontext = dbcontext;
            _env = env;
            _config = config;
        }

        [HttpGet("GetByCriteria")]

        public IActionResult GetByCriteria([FromQuery] SearchEmployeeDto searchemp)
        {
            try
            {

                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var result = from emp in _dbcontext.Employees
                             from dep in _dbcontext.Departments.Where(x => x.Id == emp.DepartmentId).DefaultIfEmpty()
                             from man in _dbcontext.Employees.Where(x => x.Id == emp.ManagerId).DefaultIfEmpty()
                             from Lookup in _dbcontext.Lookups.Where(x => x.Id == emp.PositionId)
                             where
                             (searchemp.PositionId == null || emp.PositionId == searchemp.PositionId) &&
                             (searchemp.Name == null || emp.FirstName.ToUpper().Contains(searchemp.Name.ToUpper())) &&
                             (searchemp.Status == null || emp.Status == searchemp.Status)
                             orderby emp.Id descending
                             select new EmployeeDto
                             {
                                 Id = emp.Id,
                                 Name = emp.FirstName + " " + emp.LastName,
                                 Email = emp.Email,
                                 PositionId = emp.PositionId,
                                 PositionName = Lookup.Name,
                                 BirthDay = emp.BirthDate,
                                 Salary = emp.Salary,
                                 DepartmentId = emp.DepartmentId,
                                 DepartmentName = dep.Name,
                                 ManagerName = man.FirstName,
                                 ManagerId = emp.ManagerId,
                                 UserId = emp.UserId,
                                 Status = emp.Status,
                                 ImagePath= emp.ImagePath!=null? Path.Combine(_config["BaseUrl"],emp.ImagePath):""


                             };
                if (role?.ToUpper() != "HR" && role?.ToUpper() != "Admin")
                {
                    //result = result.Where(x => x.UserId == long.Parse( userId) );
                }

                return Ok(result);
            }

            catch (Exception ex)
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
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                    PositionId = x.PositionId,
                    PositionName = x.Lookup.Name,
                    BirthDay = x.BirthDate,
                    Salary = x.Salary,
                    DepartmentId = x.DepartmentId,
                    DepartmentName = x.Department.Name,
                    ManagerId = x.ManagerId,
                    ManagerName = x.Manager.FirstName,
                    UserId = x.UserId,
                    Status = x.Status,
                    ImagePath=x.ImagePath
                    
                }).FirstOrDefault(x => x.Id == id);
                if (result == null)
                {
                    return NotFound("Employee Not Found");
                }
                if (role?.ToUpper() != "HR" && role?.ToUpper() != "Admin")
                {
                    // if (result.UserId != long.Parse(userId))
                    //  {
                    // return Forbid();   
                    //}
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles ="HR,Admin")]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] SaveEmployeeDto employeeDto)
        {
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
                    User = user,
                    Status = employeeDto.Status,
                    ImagePath=employeeDto.Image != null? UploadeImage(employeeDto.Image): null 

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
        //[Authorize(Roles = "HR,Admin")]
        [HttpPut("Update")]
        public IActionResult Update([FromForm] SaveEmployeeDto employeeDto)
        {
            var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == employeeDto.Id);

            if (employee == null)
            {
                return NotFound("employee not found");
            }
            try
            {
                employee.FirstName = employeeDto.FirstName;
                employee.LastName = employeeDto.LastName;
                employee.Email = employeeDto.Email;
                employee.PositionId = employeeDto.PositionId;
                employee.BirthDate = employeeDto.BirthDay;
                employee.DepartmentId = employeeDto.DepartmentId;
                employee.ManagerId = employeeDto.ManagerId;
                employee.Status = employeeDto.Status;

                if (employeeDto.Image != null ) 
                {
                    employee.ImagePath = UploadeImage(employeeDto.Image);
                }else if(employeeDto.Image == null && employeeDto.IsImage == false)
                {
                    employee.ImagePath = null;
                }

                    _dbcontext.SaveChanges();
                return Ok(employee);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Employee Does Not Exist");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            ;






        }
        //[Authorize(Roles = "HR,Admin")]
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var employee = _dbcontext.Employees.FirstOrDefault(x => x.Id == id);
                if (employee == null)
                {
                    return NotFound("employee not found");
                }
                var employeeManager = _dbcontext.Employees.Any(x => x.ManagerId == id);
                if (employeeManager)
                {
                    return BadRequest("You Can't Delete This Employee Because He/She Is A Manager To Other Employees");
                }
                _dbcontext.Employees.Remove(employee);
                _dbcontext.SaveChanges();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            ;

        }

        [HttpGet("GetManagers")]
        public IActionResult GetManagers()
        {
            try
            {
                var data = from emp in _dbcontext.Employees
                           from Lookup in _dbcontext.Lookups.Where(x => x.Id == emp.PositionId)
                           where Lookup.MajorCode == (int)LookupMajorCodes.Position && Lookup.MinorCode == (int)PositionMinorCodes.Manager
                           select new ListDto
                           {
                               Id = emp.Id,
                               Name = emp.FirstName + " " + emp.LastName
                           };


                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);



            }


        }
        private string UploadeImage(IFormFile Image) {
            //var employeeImagesPath = "C:\\Users\\Yasir\\Pictures\\Attachments";
            var employeeImagesPath = Path.Combine( "Attachments","Employees-Img");
            var wwwrootPath = Path.Combine(_env.WebRootPath,employeeImagesPath);
            if (!Directory.Exists(wwwrootPath))
            {
                Directory.CreateDirectory(wwwrootPath);
            }
            var fileExtintion = Path.GetExtension(Image.FileName);
            var fileName = Guid.NewGuid()+fileExtintion;
            var filePath = Path.Combine(wwwrootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create)) {

                Image.CopyTo(stream);
            } ;
            return Path.Combine(employeeImagesPath,fileName);
        }

    }

   
}