namespace HRMS_2.Models
{
    public class User
    {   public long Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword {  get; set; }
        public bool IsAdmid { get; set; }
    }
}
