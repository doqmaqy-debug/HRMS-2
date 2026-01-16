using HRMS_2.DBcontexts;
using HRMS_2.Dtos.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace HRMS_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpsController : ControllerBase
    {
        private HRMS_2Context _dbcontext;
        public LookUpsController(HRMS_2Context dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet("GetByMajorCode")]
        public IActionResult GetByMajorCode([FromQuery] int MajorCode)
        {
            try
            {
                var data = from lookup in _dbcontext.Lookups
                           where lookup.MajorCode == MajorCode && lookup.MinorCode != 0
                           select new ListDto
                           {
                               Id = lookup.Id,
                               Name = lookup.Name
                           };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
    }
}

    
