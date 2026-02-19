import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { PayrollRecord } from '../models/payroll.model';

@Injectable({
  providedIn: 'root'
})
export class PayrollService {
  private apiUrl = `${environment.apiUrl}/Payroll`;

  constructor(private http: HttpClient) {}

  // Fulfills requirement: Automate monthly payroll generation 
  generatePayroll(employeeId: number): Observable<PayrollRecord> {
    return this.http.post<PayrollRecord>(`${this.apiUrl}/generate/${employeeId}`, {});
  }

  // Fulfills requirement: Generate comprehensive reports 
  getPayrollReports(): Observable<PayrollRecord[]> {
    return this.http.get<PayrollRecord[]>(`${this.apiUrl}/reports`);
  }
}