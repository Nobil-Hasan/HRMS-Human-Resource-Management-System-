import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { EmployeeService } from '../../services/employee';
import { Employee } from '../../models/employee.model';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './employee-list.html',
  styleUrls: ['./employee-list.scss']
})
export class EmployeeListComponent implements OnInit {
  employees: Employee[] = [];
  searchTerm: string = '';
  private searchSubject = new Subject<string>();

  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();

    // Implementation of efficient search and filter logic
    this.searchSubject.pipe(
      debounceTime(400),
      distinctUntilChanged()
    ).subscribe(term => {
      this.performSearch(term);
    });
  }

  // Fulfills requirement: Retrieval of employee records
  loadEmployees(): void {
    this.employeeService.getEmployees().subscribe({
      next: (data) => this.employees = data,
      error: (err) => console.error('Error loading employees', err)
    });
  }

  onSearchChange(term: string): void {
    this.searchSubject.next(term);
  }

  // Implementation of backend-driven search functionality
  performSearch(term: string): void {
    if (term.trim()) {
      this.employeeService.searchEmployees(term).subscribe(data => this.employees = data);
    } else {
      this.loadEmployees();
    }
  }

  // Fulfills requirement: Record management and deletion
  deleteEmployee(id: number): void {
    if (confirm('Are you sure you want to delete this employee record?')) {
      this.employeeService.deleteEmployee(id).subscribe({
        next: () => {
          // Update local state by filtering out the deleted record
          this.employees = this.employees.filter(emp => emp.id !== id);
          console.log('Employee record deleted successfully');
        },
        error: (err) => console.error('Delete operation failed', err)
      });
    }
  }
}