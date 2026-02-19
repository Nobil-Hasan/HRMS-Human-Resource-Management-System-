# Human Resource Management System (HRMS) Portal

A professional **3-tier architecture** web application designed to automate HR administrative tasks, including secure employee onboarding, salary management, and automated payroll processing with tax components.

---

## 🚀 Key Features

* **Secure Access Control**: Implements an **Angular AuthGuard** and `BehaviorSubject` logic to protect sensitive routes like Payroll and Employees, ensuring robust session management.
* **Payroll Automation**: Automatically calculates **15% tax deductions** and net salaries. For instance, a gross pay of **$78,000.00** triggers a **$11,700.00** tax component calculation.
* **Automated Email Service**: Integrated **MailKit/MimeKit** service to send professional welcome emails during employee onboarding and automated payslips directly to staff inboxes.
* **Professional Payslip Management**: Includes a dedicated modal view for payroll records with built-in **Print/Save as PDF** functionality.



---

## 🛠️ Tech Stack

### **Frontend**
* **Framework**: Angular (Standalone Components).
* **Styling**: Bootstrap 5 with custom SCSS.
* **State Management**: RxJS (BehaviorSubject) for real-time authentication state.

### **Backend (BLL & API)**
* **Framework**: ASP.NET Core Web API.
* **Architecture**: 3-Tier (Presentation, Business Logic, and Data Access layers).
* **Email Engine**: MailKit & MimeKit for SMTP server integration.

### **Database**
* **Engine**: Microsoft SQL Server.

---

## 📂 Project Structure

```text
HRMS-Human-Resource-Management-System/
├── HRMS_Frontend/          # Angular Presentation Layer
│   ├── src/app/guards/    # Route protection (AuthGuard)
│   └── src/app/services/  # API & Authentication services
└── HRMS_Backend/           # .NET Business & Data Layers
    ├── HRMS.API/          # Controllers (Auth, Employee, Payroll)
    └── HRMS.BLL/          # Logic (Tax Calculations & Email services)
