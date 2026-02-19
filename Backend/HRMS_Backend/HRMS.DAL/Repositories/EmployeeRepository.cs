using HRMS.DAL.Data;
using HRMS.DAL.Entities;
using HRMS.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRMS.DAL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> SearchEmployeesAsync(string searchTerm)
        {
            return await _context.Employees
                .Where(e => e.Name.Contains(searchTerm) ||
                            e.Department.Contains(searchTerm) ||
                            e.Position.Contains(searchTerm))
                .ToListAsync();
        }
    }
}