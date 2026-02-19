using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.DTOs.Salary
{
    public class SalaryUpdateDto
    {
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonuses { get; set; }
        public decimal Deductions { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}
