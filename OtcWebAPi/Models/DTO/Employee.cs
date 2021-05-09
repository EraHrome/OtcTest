using OtcWebAPi.Interfaces;

namespace OtcWebAPi.Models.DTO
{
    public class Employee : IEmployee
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

        public int DepartmentId { get; set; }

    }
}
