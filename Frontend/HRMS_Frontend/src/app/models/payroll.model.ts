export interface PayrollRecord {
  employeeId: number;
  employeeName: string; // Fetched via the Join logic we added to the BLL
  payPeriod: Date;
  grossPay: number;
  taxAmount: number;
  netPay: number;
  summaryReport: string; // The professional payslip string 
}