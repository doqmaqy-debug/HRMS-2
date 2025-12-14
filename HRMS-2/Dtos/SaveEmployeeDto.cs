namespace HRMS_2.Dtos
{
    public class SaveEmployeeDto
    {
        public long ?Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string? Email { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
