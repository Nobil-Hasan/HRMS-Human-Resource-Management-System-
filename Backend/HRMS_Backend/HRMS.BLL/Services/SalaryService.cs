using HRMS.BLL.DTOs.Salary;
using HRMS.BLL.Interfaces;
using HRMS.DAL.Entities;
using HRMS.DAL.Interfaces;

namespace HRMS.BLL.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly IGenericRepository<Salary> _salaryRepository;

        public SalaryService(IGenericRepository<Salary> salaryRepository)
        {
            _salaryRepository = salaryRepository;
        }

        public async Task<SalaryUpdateDto> GetSalaryByEmployeeIdAsync(int employeeId)
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var salary = salaries.FirstOrDefault(s => s.EmployeeId == employeeId);

            if (salary == null) return null;

            return new SalaryUpdateDto
            {
                EmployeeId = salary.EmployeeId,
                BaseSalary = salary.BaseSalary,
                Bonuses = salary.Bonuses,
                Deductions = salary.Deductions,
                EffectiveDate = salary.EffectiveDate
            };
        }

        public async Task UpdateSalaryAsync(SalaryUpdateDto dto)
        {
            var salaries = await _salaryRepository.GetAllAsync();
            var salary = salaries.FirstOrDefault(s => s.EmployeeId == dto.EmployeeId);

            if (salary == null)
            {
                await _salaryRepository.AddAsync(new Salary
                {
                    EmployeeId = dto.EmployeeId,
                    BaseSalary = dto.BaseSalary,
                    Bonuses = dto.Bonuses,
                    Deductions = dto.Deductions,
                    EffectiveDate = dto.EffectiveDate
                });
            }
            else
            {
                salary.BaseSalary = dto.BaseSalary;
                salary.Bonuses = dto.Bonuses;
                salary.Deductions = dto.Deductions;
                salary.EffectiveDate = dto.EffectiveDate;
                _salaryRepository.Update(salary);
            }
            await _salaryRepository.SaveAsync();
        }
    }
}