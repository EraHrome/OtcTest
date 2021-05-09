using System.Collections.Generic;
using OtcWebAPi.Models.DTO;

namespace OtcWebAPi.Models.MainPageModels
{
    public class DepartmentAndEmployees
    {

        public Department Department { get; set; }
        public List<Employee> Employees { get; set; }

    }
}
