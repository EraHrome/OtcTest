using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OtcWebAPi.Providers;
using OtcWebAPi.Models.DTO;
using OtcWebAPi.Enums;

namespace OtcWebAPi.Controllers
{
    /// <summary>
    /// Api controller for only departments requests
    /// </summary>
    [ApiController]
    [Route("api")]
    public class DepartmentController : Controller
    {

        private readonly ILogger<DepartmentController> _logger;
        private readonly DepartmentsProvider _departmentsProvider;

        public DepartmentController(ILogger<DepartmentController> logger, DepartmentsProvider departmentsProvider)
        {
            _logger = logger;
            _departmentsProvider = departmentsProvider;
        }

        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid/department")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var department = await _departmentsProvider.GetById(id);
            return Ok(department);
        }

        /// <summary>
        /// Get all departments
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall/department")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentsProvider.GetAll();
            return Ok(departments);
        }

        /// <summary>
        /// Add department to database
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [HttpPost("post/department")]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            var statusCode = await _departmentsProvider.AddDepartment(department);
            if (statusCode == PostStatusCodesEnums.Exists)
            {
                return Conflict();
            }
            return NoContent();
        }

    }

}
