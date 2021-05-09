using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OtcWebAPi.Providers;

namespace OtcWebAPi.Controllers
{

    /// <summary>
    /// Api controller for mix requests
    /// </summary>
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly MixedRequestsProvider _mixedRequestsProvider;

        public HomeController(ILogger<HomeController> logger, MixedRequestsProvider mixedRequestsProvider)
        {
            _logger = logger;
            _mixedRequestsProvider = mixedRequestsProvider;
        }

        /// <summary>
        /// Get employees with department id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("getmaininfo")]
        public async Task<IActionResult> GetEmployeesWithDepartmentId(int departmentId)
        {
            var department = await _mixedRequestsProvider.GetDepartmentAndEmployees(departmentId);
            if (department != null)
            {
                return Ok(department);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get statistic info by department id
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("getstatisticinfo")]
        public async Task<IActionResult> GetStatisticInfoByDepartmentId(int departmentId)
        {
            var info = await _mixedRequestsProvider.GetStatisticInfoByDepartmentId(departmentId);
            if (info != null)
            {
                return Ok(info);
            }
            return BadRequest();
        }

        /// <summary>
        /// Get statistic info by department id with employees 
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [HttpGet("getstatisticinfowithemployees")]
        public async Task<IActionResult> GetStatisticInfoByDepartmentIdWithEmployees(int departmentId)
        {
            var info = await _mixedRequestsProvider.GetStatisticInfoByDepartmentIdWithEmployees(departmentId);
            if (info != null)
            {
                return Ok(info);
            }
            return BadRequest();
        }

    }
}
