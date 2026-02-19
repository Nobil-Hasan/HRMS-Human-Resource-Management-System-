using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; } // Replaces generic ContactDetails
        public string PhoneNumber { get; set; } // Replaces generic ContactDetails
        public string Position { get; set; }

        public string Department { get; set; }
        public string AccountNumber { get; set; }
        public string EmploymentStatus { get; set; }

        // Navigation Properties
        public ICollection<Salary> Salaries { get; set; }
        public ICollection<Payroll> Payrolls { get; set; }
    }
}
