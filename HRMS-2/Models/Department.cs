using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMS_2.Models
{
    public class Department
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        public int Floornumber {  get; set; }
       


    }
}
