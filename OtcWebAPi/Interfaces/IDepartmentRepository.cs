using System.Collections.Generic;
using System.Threading.Tasks;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Enums;

namespace OtcWebAPi.Interfaces
{
    public interface IDepartmentRepository 
    {
        Task<List<Department>> GetAllDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<PostStatusCodesEnums> AddDepartment(IDepartment model);
        Task UpdateDepartmentSalary(int departmentId);
    }
}
