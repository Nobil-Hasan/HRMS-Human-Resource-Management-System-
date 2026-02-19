import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PayrollService } from '../../services/payroll';
import { EmployeeService } from '../../services/employee';
import { PayrollRecord } from '../../models/payroll.model';
import { Employee } from '../../models/employee.model';

@Component({
  selector: 'app-payroll',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './payroll.html',
  styleUrls: ['./payroll.scss']
})
export class PayrollComponent implements OnInit {
  employees: Employee[] = [];
  reports: PayrollRecord[] = [];
  selectedReport: PayrollRecord | null = null; // Changed from string to object
  selectedEmployeeId: number | null = null;
  today: Date = new Date(); // Added for the payslip date

  constructor(
    private payrollService: PayrollService,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
    this.loadReports();
  }

  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (data) => this.employees = data,
      error: (err) => console.error('Failed to load employees', err)
    });
  }

  loadReports(): void {
    this.payrollService.getPayrollReports().subscribe({
      next: (data) => this.reports = data,
      error: (err) => console.error('Failed to load payroll reports', err)
    });
  }

  runPayroll(): void {
    if (this.selectedEmployeeId) {
      this.payrollService.generatePayroll(this.selectedEmployeeId).subscribe({
        next: () => {
          alert('Payroll generated and tax components (15%) calculated successfully.');
          this.loadReports(); 
          this.selectedEmployeeId = null;
        },
        error: () => alert('Error: Ensure employee has a valid salary profile first.')
      });
    } else {
      alert('Please select an employee first.');
    }
  }

  // Captures the full report object for the modal view
  viewSummary(report: PayrollRecord): void {
    this.selectedReport = report;
  }

  // Utility to close modal
  closeModal(): void {
    this.selectedReport = null;
  }

  // Opens the browser print dialog
  printPayslip(): void {
    window.print();
  }
}