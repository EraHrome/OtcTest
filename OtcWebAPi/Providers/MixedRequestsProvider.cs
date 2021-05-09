using OtcWebAPi.Models.MainPageModels;
using Microsoft.Extensions.Logging;
using OtcWebAPi.Repositories;
using System.Threading.Tasks;
using System.Linq;

namespace OtcWebAPi.Providers
{
    public class MixedRequestsProvider
    {
        private readonly ILogger<MixedRequestsProvider> _logger;
        private readonly DapperSqlRepository _repository;
        private readonly EmployeesProvider _employeesProvider;
        private readonly DepartmentsProvider _departmentsProvider;

        public MixedRequestsProvider(ILogger<MixedRequestsProvider> logger,
            DapperSqlRepository repository, EmployeesProvider employeesProvider, DepartmentsProvider departmentsProvider)
        {
            _logger = logger;
            _repository = repository;
            _employeesProvider = employeesProvider;
            _departmentsProvider = departmentsProvider;
        }

        public async Task<DepartmentAndEmployees> GetDepartmentAndEmployees(int departmentId)
        {
            var department = await _departmentsProvider.GetById(departmentId);
            if (department == null)
            {
                return null;
            }
            var employees = await _employeesProvider.GetAllByDepartmentId(departmentId);
            return new DepartmentAndEmployees()
            {
                Department = department,
                Employees = employees
            };
        }

        public async Task<StatisticInfo> GetStatisticInfoByDepartmentId(int departmentId)
        {
            var department = await _departmentsProvider.GetById(departmentId);
            if (department == null)
            {
                return null;
            }
            var employees = await _employeesProvider.GetAllByDepartmentId(departmentId);
            var employeesCount = employees.Count;
            var middleSalary = department.Salary / (employeesCount == 0 ? 1 : employeesCount);
            return new StatisticInfo()
            {
                DepartmentName = department.Name,
                EmployeesCount = employeesCount,
                MiddleSalary = middleSalary
            };
        }

        public async Task<StatisticInfoWithEmployees> GetStatisticInfoByDepartmentIdWithEmployees(int departmentId)
        {
            var department = await _departmentsProvider.GetById(departmentId);
            if (department == null)
            {
                return null;
            }
            var employees = await _employeesProvider.GetAllByDepartmentId(departmentId);
            var employeesCount = employees.Count;
            var middleSalary = department.Salary / (employeesCount == 0 ? 1 : employeesCount);
            return new StatisticInfoWithEmployees()
            {
                DepartmentName = department.Name,
                EmployeesCount = employeesCount,
                Employees = employees,
                MiddleSalary = middleSalary
            };
        }

    }
}
