using HRMS.BLL.DTOs.Employee;
using HRMS.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        // Injecting the Email Service for onboarding notifications
        private readonly IEmailService _emailService;

        public EmployeeController(IEmployeeService employeeService, IEmailService emailService)
        {
            _employeeService = employeeService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _employeeService.GetAllEmployeesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _employeeService.GetEmployeeByIdAsync(id));

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string term) => Ok(await _employeeService.SearchEmployeesAsync(term));

        [HttpPost]
        public async Task<IActionResult> Create(CreateEmployeeDto dto)
        {
            // 1. Add employee to the SQL database via BLL
            await _employeeService.AddEmployeeAsync(dto);

            try
            {
                // 2. Trigger Automated Welcome Email
                // We reuse the interface but send onboarding content instead of payroll data
                await _emailService.SendWelcomeEmailAsync(dto.Email, dto.Name);

                return Ok(new { message = "Employee created and welcome email sent successfully" });
            }
            catch (System.Exception ex)
            {
                // Returns success for creation, but warns about the email failure
                return Ok(new { message = "Employee created, but welcome email failed: " + ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateEmployeeDto dto)
        {
            await _employeeService.UpdateEmployeeAsync(id, dto);
            return Ok(new { message = "Employee updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}