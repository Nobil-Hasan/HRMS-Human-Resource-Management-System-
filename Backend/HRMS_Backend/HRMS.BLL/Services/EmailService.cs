using HRMS.BLL.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

// Resolves CS0104 error by explicitly choosing the MailKit client
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendWelcomeEmailAsync(string toEmail, string employeeName)
    {
        var message = new MimeMessage();
        // Uses the Sender address from appsettings.json
        message.From.Add(new MailboxAddress("AIUB HRMS Portal", _config["EmailSettings:Sender"]));
        message.To.Add(new MailboxAddress(employeeName, toEmail));
        message.Subject = "Welcome to AIUB HRMS Portal";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $@"
                <div style='font-family: Arial, sans-serif; padding: 20px; border: 1px solid #eee;'>
                    <h2 style='color: #0d6efd;'>Welcome aboard, {employeeName}!</h2>
                    <p>Your profile has been successfully created in the <strong>AIUB HRMS Portal</strong>.</p>
                    <p>You can now log in to view your profile and manage your records.</p>
                    <br/>
                    <p>Best Regards,<br/><strong>Nahid Hasan Nobil</strong><br/>HR Administration</p>
                </div>"
        };
        message.Body = bodyBuilder.ToMessageBody();

        await SendEmailAsync(message);
    }

    // Generic helper to handle SMTP connection logic
    private async Task SendEmailAsync(MimeMessage message)
    {
        using (var client = new SmtpClient())
        {
            // Connect using your app password: cxla bqls rnew xudq
            await client.ConnectAsync(_config["EmailSettings:Host"], 587, MailKit.Security.SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_config["EmailSettings:User"], _config["EmailSettings:Password"]);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }

    public async Task SendPayslipEmailAsync(string toEmail, string employeeName, decimal gross, decimal tax, decimal net)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("AIUB HRMS Portal", _config["EmailSettings:Sender"]));
        message.To.Add(new MailboxAddress(employeeName, toEmail));
        message.Subject = $"Monthly Payslip - {DateTime.Now:MMMM yyyy}";

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = $"<p>Dear {employeeName}, your net pay is {net:C}.</p>"
        };
        message.Body = bodyBuilder.ToMessageBody();

        await SendEmailAsync(message);
    }
}