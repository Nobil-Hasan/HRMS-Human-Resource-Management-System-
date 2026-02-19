using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.DTOs.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string AccountNumber { get; set; }
        public string EmploymentStatus { get; set; }
    }
}
