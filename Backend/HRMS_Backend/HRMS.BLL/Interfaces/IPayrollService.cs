using HRMS.BLL.DTOs.Payroll;

namespace HRMS.BLL.Interfaces
{
    public interface IPayrollService
    {
        Task<PayrollResultDto> GenerateMonthlyPayrollAsync(int employeeId);
        Task<IEnumerable<PayrollResultDto>> GetAllPayrollReportsAsync();
    }
}