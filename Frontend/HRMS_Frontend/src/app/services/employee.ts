import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs'; // Added for error handling
import { environment } from '../../environments/environment';
import { Employee } from '../models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = `${environment.apiUrl}/Employee`;

  constructor(private http: HttpClient) {}

  // Fulfills requirement: Retrieval of employee records
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl).pipe(
      catchError(this.handleError)
    );
  }

  // Critical for Edit Mode and Salary Revision logic
  getEmployeeById(id: number): Observable<Employee> {
    return this.http.get<Employee>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // Fulfills requirement: Search and filter functionalities
  searchEmployees(term: string): Observable<Employee[]> {
    const params = new HttpParams().set('term', term);
    return this.http.get<Employee[]>(`${this.apiUrl}/search`, { params }).pipe(
      catchError(this.handleError)
    );
  }

  // Fulfills requirement: Add employee records to SQL via BLL
  addEmployee(employee: Employee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee).pipe(
      catchError(this.handleError)
    );
  }

  // Fulfills requirement: Edit employee records
  updateEmployee(id: number, employee: Employee): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, employee).pipe(
      catchError(this.handleError)
    );
  }

  // Fulfills requirement: Record management and deletion
  deleteEmployee(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(this.handleError)
    );
  }

  // Centralized error handling for a user-friendly experience
  private handleError(error: any) {
    console.error('An API error occurred:', error);
    return throwError(() => new Error('Something went wrong; please try again later.'));
  }
}