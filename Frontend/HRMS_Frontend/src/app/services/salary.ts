import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Salary } from '../models/salary.model';

@Injectable({
  providedIn: 'root'
})
export class SalaryService {
  private apiUrl = `${environment.apiUrl}/Salary`;

  constructor(private http: HttpClient) {}

  // Fulfills requirement: Enable HR to get employee salaries 
  getSalaryByEmployeeId(employeeId: number): Observable<Salary> {
    return this.http.get<Salary>(`${this.apiUrl}/${employeeId}`);
  }

  // Fulfills requirement: Set and update salaries, revisions, and bonuses 
  updateSalary(salaryData: Salary): Observable<any> {
    return this.http.post(`${this.apiUrl}/update`, salaryData);
  }
}