using HRMS.BLL.DTOs.Salary;

namespace HRMS.BLL.Interfaces
{
    public interface ISalaryService
    {
        Task<SalaryUpdateDto> GetSalaryByEmployeeIdAsync(int employeeId);
        Task UpdateSalaryAsync(SalaryUpdateDto salaryDto);
    }
}