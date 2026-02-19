import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  
  // Dashboard / Employee Directory protected by Guard
  { 
    path: 'employees', 
    loadComponent: () => import('./components/employee-list/employee-list').then(m => m.EmployeeListComponent),
    canActivate: [authGuard] 
  },
  
  // Unified Employee Form for Add/Edit
  { 
    path: 'employees/add', 
    loadComponent: () => import('./components/employee-form/employee-form').then(m => m.EmployeeFormComponent),
    canActivate: [authGuard] 
  },
  { 
    path: 'employees/edit/:id', 
    loadComponent: () => import('./components/employee-form/employee-form').then(m => m.EmployeeFormComponent),
    canActivate: [authGuard] 
  },

  // Salary Revision Module
  { 
    path: 'salary/:id', 
    loadComponent: () => import('./components/salary-management/salary-management').then(m => m.SalaryManagementComponent),
    canActivate: [authGuard] 
  },
  
  // Payroll Automation and Reporting
  { 
    path: 'payroll', 
    loadComponent: () => import('./components/payroll/payroll').then(m => m.PayrollComponent),
    canActivate: [authGuard] 
  },
  
  // Default and Wildcard Routing
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' } 
];