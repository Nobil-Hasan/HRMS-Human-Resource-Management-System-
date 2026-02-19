import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router'; 
import { SalaryService } from '../../services/salary';
import { EmployeeService } from '../../services/employee';

@Component({
  selector: 'app-salary-management',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink], 
  templateUrl: './salary-management.html'
})
export class SalaryManagementComponent implements OnInit {
  salaryForm: FormGroup;
  employeeId!: number;
  employeeName: string = '';

  constructor(
    private fb: FormBuilder,
    private salaryService: SalaryService,
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.salaryForm = this.fb.group({
      employeeId: [null],
      baseSalary: [0, [Validators.required, Validators.min(0)]], 
      bonuses: [0, [Validators.min(0)]],    
      deductions: [0, [Validators.min(0)]], 
      effectiveDate: ['', [Validators.required]] 
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.employeeId = Number(idParam);
      this.salaryForm.patchValue({ employeeId: this.employeeId });
      this.loadEmployeeDetails();
      this.loadSalaryData();
    }
  }

  loadEmployeeDetails() {
    this.employeeService.getEmployees().subscribe(emps => {
      const emp = emps.find(e => e.id === this.employeeId);
      if (emp) this.employeeName = emp.name;
    });
  }

  loadSalaryData() {
    this.salaryService.getSalaryByEmployeeId(this.employeeId).subscribe(data => {
      if (data) {
        // Strip time component for standard HTML date input compatibility
        const formattedData = { 
          ...data, 
          effectiveDate: data.effectiveDate ? data.effectiveDate.split('T')[0] : '' 
        };
        this.salaryForm.patchValue(formattedData);
      }
    });
  }

  onSubmit() {
    if (this.salaryForm.valid) {
      // FIX: Consolidate data into a single payload object for the service
      const payload = { ...this.salaryForm.value, employeeId: this.employeeId };
      this.salaryService.updateSalary(payload).subscribe({
        next: () => {
          alert('Salary records updated successfully'); 
          this.router.navigate(['/employees']);
        },
        error: (err) => console.error('Update failed', err)
      });
    }
  }
}