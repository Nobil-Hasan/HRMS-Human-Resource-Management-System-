using System.Threading.Tasks;

namespace HRMS.BLL.Interfaces
{
    public interface IEmailService
    {
        // Method for Onboarding
        Task SendWelcomeEmailAsync(string toEmail, string employeeName);

        // Optional: Keep for future payroll use
        Task SendPayslipEmailAsync(string toEmail, string employeeName, decimal gross, decimal tax, decimal net);
    }
}