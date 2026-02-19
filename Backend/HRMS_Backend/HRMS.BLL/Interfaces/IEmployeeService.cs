using HRMS.BLL.DTOs.Employee;

namespace HRMS.BLL.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm);
        Task AddEmployeeAsync(CreateEmployeeDto employeeDto);
        Task UpdateEmployeeAsync(int id, CreateEmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
    }
}