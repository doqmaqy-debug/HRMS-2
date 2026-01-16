namespace HRMS_2.Dtos
{
    public class SaveEmployeeDto
    {
        public long ?Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PositionId { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDay { get; set; }
        public decimal Salary { get; set; }
        public long? DepartmentId { get; set; }
        public long? ManagerId { get; set; }
        public bool Status { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsImage { get; set; }

    }
}
