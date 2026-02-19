using HRMS.BLL.DTOs.Employee;
using HRMS.BLL.Interfaces;
using HRMS.DAL.Entities;
using HRMS.DAL.Interfaces;

namespace HRMS.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Position = e.Position,
                Department = e.Department,
                AccountNumber = e.AccountNumber,
                EmploymentStatus = e.EmploymentStatus
            });
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return null;

            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Position = employee.Position,
                Department = employee.Department,
                AccountNumber = employee.AccountNumber,
                EmploymentStatus = employee.EmploymentStatus
            };
        }

        public async Task<IEnumerable<EmployeeDto>> SearchEmployeesAsync(string searchTerm)
        {
            var employees = await _employeeRepository.SearchEmployeesAsync(searchTerm);
            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Position = e.Position,
                Department = e.Department,
                AccountNumber = e.AccountNumber,
                EmploymentStatus = e.EmploymentStatus
            });
        }

        public async Task AddEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Position = dto.Position,
                Department = dto.Department,
                AccountNumber = dto.AccountNumber,
                EmploymentStatus = dto.EmploymentStatus
            };
            await _employeeRepository.AddAsync(employee);
            await _employeeRepository.SaveAsync();
        }

        public async Task UpdateEmployeeAsync(int id, CreateEmployeeDto dto)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                employee.Name = dto.Name;
                employee.Email = dto.Email;
                employee.PhoneNumber = dto.PhoneNumber;
                employee.Position = dto.Position;
                employee.Department = dto.Department;
                employee.AccountNumber = dto.AccountNumber;
                employee.EmploymentStatus = dto.EmploymentStatus;

                _employeeRepository.Update(employee);
                await _employeeRepository.SaveAsync();
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee != null)
            {
                _employeeRepository.Delete(employee);
                await _employeeRepository.SaveAsync();
            }
        }
    }
}