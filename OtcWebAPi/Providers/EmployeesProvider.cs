using Microsoft.Extensions.Logging;
using OtcWebAPi.Enums;
using OtcWebAPi.Interfaces;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtcWebAPi.Providers
{
    public class EmployeesProvider
    {

        private readonly ILogger<EmployeesProvider> _logger;
        private readonly IEmployeeRepository _repository;

        public EmployeesProvider(ILogger<EmployeesProvider> logger, DapperSqlRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<Employee>> GetAllByDepartmentId(int departmentId)
        {
            return await _repository.GetAllEmployeesByDepartmentId(departmentId);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _repository.GetAllEmployees();
        }

        public async Task<Employee> GetById(int id)
        {
            var resultModel = await _repository.GetEmployeeById(id);
            return resultModel;
        }

        public async Task<PostStatusCodesEnums> AddEmployee(IEmployee model)
        {
            var statusCode = await _repository.AddEmployee(model);
            return statusCode;
        }

    }
}
