using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Entities
{
    public class Salary
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal Bonuses { get; set; }
        public decimal Deductions { get; set; }
        public DateTime EffectiveDate { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }
    }
}
