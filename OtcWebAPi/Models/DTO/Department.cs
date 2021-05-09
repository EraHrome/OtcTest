using OtcWebAPi.Interfaces;

namespace OtcWebAPi.Models.DTO
{
    public class Department : IDepartment
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Salary { get; set; }

    }
}
