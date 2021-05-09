using Microsoft.Extensions.Logging;
using OtcWebAPi.Enums;
using OtcWebAPi.Interfaces;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OtcWebAPi.Providers
{
    public class DepartmentsProvider
    {

        private readonly ILogger<DepartmentsProvider> _logger;
        private readonly IDepartmentRepository _repository;

        public DepartmentsProvider(ILogger<DepartmentsProvider> logger, DapperSqlRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _repository.GetAllDepartments();
        }

        public async Task<Department> GetById(int id)
        {
            var resultModel = await _repository.GetDepartmentById(id);
            return resultModel;
        }

        public async Task<PostStatusCodesEnums> AddDepartment(IDepartment model)
        {
            var statusCode = await _repository.AddDepartment(model);
            return statusCode;
        }

    }
}
