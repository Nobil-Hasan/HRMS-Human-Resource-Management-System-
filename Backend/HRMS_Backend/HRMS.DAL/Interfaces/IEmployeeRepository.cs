using HRMS.DAL.Entities;

namespace HRMS.DAL.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        Task<IEnumerable<Employee>> SearchEmployeesAsync(string searchTerm);
    }
}