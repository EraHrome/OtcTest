using Dapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OtcWebAPi.Enums;
using OtcWebAPi.Interfaces;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Models.Options;
using OtcWebAPi.Providers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OtcWebAPi.Repositories
{
    public class DapperSqlRepository : IDepartmentRepository, IEmployeeRepository
    {

        private readonly ILogger<DapperSqlRepository> _logger;
        private readonly InputOptions _options;
        private readonly IDbConnection _dbConnection;
        private readonly ReflectionLogicsProvider _provider;

        public DapperSqlRepository(ILogger<DapperSqlRepository> logger, IOptions<InputOptions> options,
            ReflectionLogicsProvider provider)
        {
            _logger = logger;
            _options = options.Value;
            _dbConnection = new SqlConnection(_options.AzureSqlConnectionString);
            _provider = provider;
        }

        public async Task<PostStatusCodesEnums> AddDepartment(IDepartment model)
        {
            var searchModel = await GetFirstDepartmentByFields(model);
            if (searchModel != null)
            {
                return PostStatusCodesEnums.Exists;
            }
            await _dbConnection.QueryAsync("INSERT INTO Department (Name, Salary) VALUES(@Name, @Salary)", new { model.Name, model.Salary });
            return PostStatusCodesEnums.Success;
        }

        public async Task<PostStatusCodesEnums> AddEmployee(IEmployee model)
        {
            var searchModel = await GetFirstEmployeeByFields(model);
            if (searchModel != null)
            {
                return PostStatusCodesEnums.Exists;
            }
            var department = await GetDepartmentById(model.DepartmentId);
            if (department == null)
            {
                return PostStatusCodesEnums.BadRequest;
            }
            await _dbConnection.QueryAsync("INSERT INTO Employee (Name, Salary, DepartmentId) VALUES(@Name, @Salary, @DepartmentId)", new { model.Name, model.Salary, model.DepartmentId });
            await UpdateDepartmentSalary(department.Id);
            return PostStatusCodesEnums.Success;
        }

        public async Task<List<Department>> GetAllDepartments()
        {
            var departments = await _dbConnection.QueryAsync<Department>("SELECT * FROM Department");
            return departments.ToList();
        }

        public async Task<List<Employee>> GetAllEmployeesByDepartmentId(int id)
        {
            var employees = await _dbConnection.QueryAsync<Employee>("SELECT * FROM Employee WHERE DepartmentId = @id", new { id });
            return employees.ToList();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var employees = await _dbConnection.QueryAsync<Employee>("SELECT * FROM Employee");
            return employees.ToList();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var resultModel = await _dbConnection.QueryAsync<Department>($"SELECT TOP 1 * FROM Department WHERE Id = @id", new { id });
            return resultModel.FirstOrDefault();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var resultModel = await _dbConnection.QueryAsync<Employee>($"SELECT TOP 1 * FROM Employee WHERE Id = @id", new { id });
            return resultModel.FirstOrDefault();
        }

        public async Task<Department> GetFirstDepartmentByFields(IDepartment model)
        {
            var whereMessage = _provider.GenerateWhereMessageFromFields(model);
            if (String.IsNullOrEmpty(whereMessage))
            {
                return null;
            }
            var queryResult = await _dbConnection.QueryAsync<Department>($"SELECT * FROM Department WHERE {whereMessage}");
            return queryResult.FirstOrDefault();
        }

        public async Task<Employee> GetFirstEmployeeByFields(IEmployee model)
        {
            var whereMessage = _provider.GenerateWhereMessageFromFields(model);
            if (String.IsNullOrEmpty(whereMessage))
            {
                return null;
            }
            var queryResult = await _dbConnection.QueryAsync<Employee>($"SELECT * FROM Employee WHERE {whereMessage}");
            return queryResult.FirstOrDefault();
        }

        public async Task UpdateDepartmentSalary(int departmentId)
        {
            await _dbConnection.QueryAsync("UPDATE Department SET Salary = (SELECT SUM(Salary) FROM Employee WHERE DepartmentId = @departmentId) WHERE Id = @departmentId", new { departmentId });
        }

    }
}
