using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OtcWebAPi.Providers;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Enums;

namespace OtcWebAPi.Controllers
{

    /// <summary>
    /// Api controller for only employees requests
    /// </summary>
    [ApiController]
    [Route("api")]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeesProvider _employeesProvider;

        public EmployeeController(ILogger<EmployeeController> logger, EmployeesProvider employeesProvider)
        {
            _logger = logger;
            _employeesProvider = employeesProvider;
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/employee")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var employee = await _employeesProvider.GetById(id);
            return Ok(employee);
        }

        /// <summary>
        /// Get all employees
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall/employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeesProvider.GetAll();
            return Ok(employees);
        }

        /// <summary>
        /// Add employee to database
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost("post/employee")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var statusCode = await _employeesProvider.AddEmployee(employee);
            if (statusCode == PostStatusCodesEnums.Exists)
            {
                return Conflict();
            }
            if(statusCode == PostStatusCodesEnums.BadRequest)
            {
                return BadRequest();
            }
            return NoContent();
        }

    }
}
