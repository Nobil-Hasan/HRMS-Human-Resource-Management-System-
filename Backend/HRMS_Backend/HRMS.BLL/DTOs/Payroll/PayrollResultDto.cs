using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.BLL.DTOs.Payroll
{
    public class PayrollResultDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime PayPeriod { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetPay { get; set; }
        public string SummaryReport { get; set; }
    }
}
