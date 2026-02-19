# Human Resource Management System (HRMS) Portal

A comprehensive, full-stack **3-Tier Architecture** web application designed for end-to-end HR operations, featuring automated payroll, secure employee lifecycle management, and real-time administrative reporting.

---

## 🚀 Core Features

### 🔐 Secure Authentication & Authorization
* **JWT-Based Login**: Implements secure user authentication using JSON Web Tokens.
* **RxJS State Management**: Utilizes `BehaviorSubject` in the `AuthService` to provide real-time login status across the entire application.
* **Angular Route Guards**: Features an `authGuard` that intercepts navigation to protect sensitive modules (Payroll, Employees, Salary Management), redirecting unauthorized users back to the login page.
* **Session Persistence**: Syncs authentication state with browser storage to maintain secure sessions across page refreshes.

### 💰 Automated Payroll & Tax Engine
* **Automated Calculations**: A dedicated Business Logic Layer (BLL) calculates payroll components instantly upon processing.
* **15% Tax Automation**: Automatically triggers a mandatory **15% tax deduction** calculation for every record. 
* **Precision Reporting**: Correctly calculates complex figures (e.g., a gross pay of **$1,020,000.00** results in a **$153,000.00** tax component and a **$858,000.00** net pay).
* **Payroll History**: Maintains a persistent record of all processed payrolls in the SQL database for historical auditing.

### 📧 Integrated Communication Service
* **Onboarding Emails**: Automatically triggers a professional "Welcome" email to new staff members upon record creation.
* **SMTP Integration**: Powered by a robust **MailKit/MimeKit** implementation using secure SMTP protocols.
* **Automated Payslip Dispatch**: Backend logic configured to send detailed payroll summaries directly to employee email addresses.

### 📄 Document & Employee Management
* **Professional Payslip View**: Features an interactive modal summary for every payroll record.
* **Print & Export**: Built-in functionality to **Print or Save as PDF** directly from the browser for physical record-keeping.
* **Employee Directory**: Full CRUD (Create, Read, Update, Delete) capabilities for managing staff profiles, departments, and contact information.

---

## 🛠️ Technical Architecture



| Layer | Technology |
| :--- | :--- |
| **Frontend (Presentation)** | Angular (Standalone), Bootstrap 5, SCSS, RxJS Observables |
| **Backend (Business Logic)** | ASP.NET Core Web API, MailKit, MimeKit |
| **Data Access (DAL)** | Entity Framework Core, Repository Pattern |
| **Database** | Microsoft SQL Server |

---

## 📂 Project Structure

```text
HRMS-Human-Resource-Management-System/
├── HRMS_Frontend/           # Angular Presentation Layer
│   ├── src/app/guards/     # authGuard.ts (Secure access logic)
│   ├── src/app/services/   # auth.service.ts (State management)
│   └── src/app/components/ # Payroll, Employee, and Dashboard components
└── HRMS_Backend/            # .NET Core Logic & Data Layers
    ├── HRMS.API/           # Controllers (Auth, Employee, Payroll)
    ├── HRMS.BLL/           # Business Logic (Tax calculation & Email engines)
    └── HRMS.DAL/           # Data Access Layer (SQL Context & Repositories)
```


## ⚙️ Setup & Configuration

### 1. Backend (API & BLL)
Create an `appsettings.json` file in the `HRMS.API` folder. This file is ignored by Git to protect sensitive SMTP credentials:

```json
{
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Sender": "your-email@gmail.com",
    "User": "your-email@gmail.com",
    "Password": "your-16-character-app-password"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=HRMS_DB;Trusted_Connection=True;"
  }
}
```

##2. Frontend (Angular)

```Bash
cd HRMS_Frontend/HRMS_Frontend
npm install
ng serve
```



### 👤 Author
## Nahid Hasan Nobil

# Bachelor of Science in Computer Science and Engineering form AIUB

# Full-Stack Developer specializing in .NET and Angular
