using HRMS_2.DBcontexts;
using HRMS_2.Dtos;
using HRMS_2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;


namespace HRMS_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly HRMS_2Context _dbcontext;
        public DepartmentController(HRMS_2Context dbcontext)
        {
            _dbcontext = dbcontext;
        }


        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria([FromQuery] SearchDepartmentDto searchdep)
        {

            var result = from dep in _dbcontext.Departments
             
                         where (searchdep.Name == null || dep.Name.ToUpper().Contains(searchdep.Name.ToUpper())) &&
                         (searchdep.Description == null || searchdep.Description.ToUpper().Contains(searchdep.Description.ToUpper()))
                         select new DepartmentDto
                         {
                             Id = dep.Id,
                             Name = dep.Name,
                             Description = dep.Description,
                             Floornumber = dep.Floornumber,
                             
                          

                             

                         };
            return Ok(result);

        }
        [HttpGet("GetById")]
        public IActionResult Get(long id)
        {
            if (id == 0)
            {
                return BadRequest("Id value is not valid");
            }
            ;

            var result = _dbcontext.Departments.Select(x => new DepartmentDto
            {

                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Floornumber = x.Floornumber,
               
                
                

                

            }).FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                return NotFound("Department Not Found");
            }
            return Ok(result);



        }



        [HttpPost("Add")]
        public IActionResult Add(SaveDepartmentDto departmentdto)
        {
            var department = new Department()
            {
                //Id = (_dbcontext.Departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = departmentdto.Name,
                Description = departmentdto.Description,
                Floornumber = departmentdto.Floornumber,
                
                
                

            };
            _dbcontext.Departments.Add(department);
            _dbcontext.SaveChanges();
            return Ok();

        }
        [HttpPut("Update")]
        public IActionResult Update(SaveDepartmentDto departmentDto)
        {
            var department = _dbcontext.Departments.FirstOrDefault(x => x.Id == departmentDto.Id);
            if (department == null)
            {
                return NotFound("department Not found");
            }
            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;
            department.Floornumber = departmentDto.Floornumber;
            
            _dbcontext.SaveChanges();
            return Ok(department);



        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            var department = _dbcontext.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null)
            {
                return NotFound("Department does not exist");
            }
            _dbcontext.Departments.Remove(department);
            _dbcontext.SaveChanges();
            return Ok(department);
        }
    }
}
