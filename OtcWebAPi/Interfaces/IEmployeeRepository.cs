using System.Collections.Generic;
using System.Threading.Tasks;
using OtcWebAPi.Enums;
using OtcWebAPi.Models.DTO;

namespace OtcWebAPi.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<PostStatusCodesEnums> AddEmployee(IEmployee model);
        Task<List<Employee>> GetAllEmployeesByDepartmentId(int id);
    }
}
