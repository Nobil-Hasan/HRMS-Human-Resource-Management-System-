using HRMS.BLL.DTOs.Salary;
using HRMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetSalary(int employeeId) => Ok(await _salaryService.GetSalaryByEmployeeIdAsync(employeeId));

        [HttpPost("update")]
        public async Task<IActionResult> UpdateSalary(SalaryUpdateDto dto)
        {
            await _salaryService.UpdateSalaryAsync(dto);
            return Ok(new { message = "Salary records updated successfully" });
        }
    }
}