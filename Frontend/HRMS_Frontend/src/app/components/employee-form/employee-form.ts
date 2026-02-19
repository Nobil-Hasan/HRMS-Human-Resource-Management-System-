import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router'; 
import { EmployeeService } from '../../services/employee';

@Component({
  selector: 'app-employee-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink], 
  templateUrl: './employee-form.html',
  styleUrls: ['./employee-form.scss']
})
export class EmployeeFormComponent implements OnInit {
  employeeForm: FormGroup;
  isEditMode = false;
  employeeId?: number;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.employeeForm = this.fb.group({
      name: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      position: ['', [Validators.required]],
      department: ['', [Validators.required]],
      accountNumber: ['', [Validators.required]],
      employmentStatus: ['Active', [Validators.required]]
    });
  }

  ngOnInit(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.employeeId = Number(idParam);
      this.isEditMode = true;
      this.loadEmployeeData(this.employeeId);
    }
  }

  loadEmployeeData(id: number) {
    this.employeeService.getEmployees().subscribe(employees => {
      const emp = employees.find(e => e.id === id);
      if (emp) {
        this.employeeForm.patchValue(emp);
      }
    });
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      const data = this.employeeForm.value;
      
      if (this.isEditMode && this.employeeId) {
        // FIX: Pass employeeId and data as two distinct arguments to resolve compiler error
        this.employeeService.updateEmployee(this.employeeId, data).subscribe({
          next: () => {
            alert('Employee updated successfully!');
            this.router.navigate(['/employees']);
          },
          error: (err) => console.error('Update failed', err)
        });
      } else {
        this.employeeService.addEmployee(data).subscribe({
          next: () => {
            alert('Employee added successfully!');
            this.router.navigate(['/employees']);
          },
          error: (err) => console.error('Add failed', err)
        });
      }
    }
  }
}