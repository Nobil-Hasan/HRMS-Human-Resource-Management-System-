using HRMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        [HttpPost("generate/{employeeId}")]
        public async Task<IActionResult> Generate(int employeeId)
        {
            var result = await _payrollService.GenerateMonthlyPayrollAsync(employeeId);
            return Ok(result);
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetReports() => Ok(await _payrollService.GetAllPayrollReportsAsync());
    }
}