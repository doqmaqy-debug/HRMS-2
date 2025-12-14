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
        public static List<Department> departments = new List<Department>() {
            new Department(){Id =1 , Name="Human Resourses",Description="Human Resourses Department",Floornumber=1 },
            new Department(){Id =2 , Name="Finance",Description="Finance Department",Floornumber=2},
            new Department(){Id =3 , Name="Development",Description="Development Department",Floornumber=3},

        };

        [HttpGet("GetByCriteria")]
        public IActionResult GetByCriteria([FromQuery] SearchDepartmentDto searchdep ) {

            var result = from dep in departments
                         where (searchdep.Name == null || dep.Name.ToUpper().Contains( searchdep.Name.ToUpper()))&&
                         (searchdep.Description == null || searchdep.Description.ToUpper().Contains( searchdep.Description.ToUpper()))
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
        public IActionResult Get(long id) {
            var result = departments.Select(x => new DepartmentDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Floornumber = x.Floornumber,

            }).FirstOrDefault(x => x.Id == id);
            return Ok(result);



        }



        [HttpPost("Add")]
        public IActionResult Add(SaveDepartmentDto departmentdto) {
            var department = new Department() {
                Id = (departments.LastOrDefault()?.Id ?? 0) + 1,
                Name = departmentdto.Name,
                Description = departmentdto.Description,
                Floornumber = departmentdto.Floornumber,

            };
            departments.Add(department);
            return Ok();

        }
        [HttpPut("Update")]
        public IActionResult Update(SaveDepartmentDto departmentDto) {
           var department = departments.FirstOrDefault(x => x.Id == departmentDto.Id);
            if (department == null) {
                return NotFound("department Not found"); 
           }
            department.Name = departmentDto.Name;
            department.Description = departmentDto.Description;
            department.Floornumber = departmentDto.Floornumber;
            return Ok(department);


           
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            var department = departments.FirstOrDefault(x=>x.Id ==id);
            if (department == null)
            {
                return NotFound("Department does not exist");
            }
            departments.Remove(department);
            return Ok(department);
        }
    }
}
