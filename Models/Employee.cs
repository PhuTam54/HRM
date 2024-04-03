namespace HRM.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Date_Of_Birth { get; set; }
        public string Phone_Number { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
    }
}
