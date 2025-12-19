using HRMS_2.Models;
using Microsoft.EntityFrameworkCore;

namespace HRMS_2.DBcontexts
{
    public class HRMS_2Context :DbContext
    {
        public HRMS_2Context(DbContextOptions<HRMS_2Context>options):base(options)
        {

        
        }

        public DbSet<Employee>Employees { get; set; }
        public DbSet<Department>Departments { get; set; }


    }
}
