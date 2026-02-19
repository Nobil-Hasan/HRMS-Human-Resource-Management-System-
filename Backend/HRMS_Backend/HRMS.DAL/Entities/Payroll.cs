using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.DAL.Entities
{
    public class Payroll
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PayPeriod { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetPay { get; set; }

        public string SummaryReport { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }
    }
}
