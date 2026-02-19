using HRMS.BLL.DTOs.Payroll;
using HRMS.BLL.Interfaces;
using HRMS.DAL.Entities;
using HRMS.DAL.Interfaces;

namespace HRMS.BLL.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IGenericRepository<Payroll> _payrollRepository;
        private readonly IGenericRepository<Salary> _salaryRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollService(
            IGenericRepository<Payroll> payrollRepository,
            IGenericRepository<Salary> salaryRepository,
            IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _salaryRepository = salaryRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<PayrollResultDto> GenerateMonthlyPayrollAsync(int employeeId)
        {
            var employee = await _employeeRepository.GetByIdAsync(employeeId);
            var salaries = await _salaryRepository.GetAllAsync();
            var salary = salaries.FirstOrDefault(s => s.EmployeeId == employeeId);

            if (employee == null || salary == null) return null;

            decimal grossPay = salary.BaseSalary + salary.Bonuses;
            decimal taxRate = 0.15m;
            decimal taxAmount = grossPay * taxRate;
            decimal netPay = (grossPay - taxAmount) - salary.Deductions;

            // Fixed professional summary assignment
            string professionalSummary =
                $"PAYSLIP REPORT - {DateTime.Now:MMMM yyyy}\n" +
                $"------------------------------------------\n" +
                $"Employee: {employee.Name} (ID: {employee.Id})\n" +
                $"Designation: {employee.Position} | Dept: {employee.Department}\n" +
                $"Account Number: {employee.AccountNumber}\n" +
                $"------------------------------------------\n" +
                $"Earnings:\n" +
                $"  Base Salary: {salary.BaseSalary:C}\n" +
                $"  Bonuses:     {salary.Bonuses:C}\n" +
                $"Deductions:\n" +
                $"  Taxes (15%): {taxAmount:C}\n" +
                $"  Others:      {salary.Deductions:C}\n" +
                $"------------------------------------------\n" +
                $"TOTAL GROSS PAY: {grossPay:C}\n" +
                $"TOTAL NET PAY:   {netPay:C}\n" +
                $"------------------------------------------\n" +
                $"Status: {employee.EmploymentStatus}";

            var payroll = new Payroll
            {
                EmployeeId = employeeId,
                PayPeriod = DateTime.Now,
                GrossPay = grossPay,
                TaxAmount = taxAmount,
                NetPay = netPay,
                SummaryReport = professionalSummary
            };

            await _payrollRepository.AddAsync(payroll);
            await _payrollRepository.SaveAsync();

            return new PayrollResultDto
            {
                EmployeeId = employeeId,
                EmployeeName = employee.Name,
                PayPeriod = payroll.PayPeriod,
                GrossPay = grossPay,
                TaxAmount = taxAmount,
                NetPay = netPay,
                SummaryReport = professionalSummary
            };
        }

        public async Task<IEnumerable<PayrollResultDto>> GetAllPayrollReportsAsync()
        {
            // Fetch all payrolls and employees from their respective repositories
            var payrolls = await _payrollRepository.GetAllAsync();
            var employees = await _employeeRepository.GetAllAsync();

            // Use a Join to match the EmployeeId and pull the Name
            return payrolls.Join(employees,
                p => p.EmployeeId,
                e => e.Id,
                (p, e) => new PayrollResultDto
                {
                    EmployeeId = p.EmployeeId,
                    EmployeeName = e.Name, // This ensures the name is no longer null
                    PayPeriod = p.PayPeriod,
                    GrossPay = p.GrossPay,
                    TaxAmount = p.TaxAmount,
                    NetPay = p.NetPay,
                    SummaryReport = p.SummaryReport
                });
        }
    }
}