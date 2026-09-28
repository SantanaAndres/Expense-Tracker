using Application.Abstraction.Services;
using brevo_csharp.Api;
using brevo_csharp.Client;
using brevo_csharp.Model;
using Microsoft.Extensions.Configuration;
using Task = System.Threading.Tasks.Task;

namespace Infrastructure.Services;

public class EmailCommunicationService(IConfiguration configuration) : ICommunicationService
{
    public async Task SendMessage(string toEmail, string toName, string subject, string htmlContent)
    {
        var providerSenderData = configuration.GetSection("Brevo");
        
        var apiKey = providerSenderData.GetSection("ApiKey").Get<string>()!;

        var config = new brevo_csharp.Client.Configuration { ApiKey = { ["api-key"] = apiKey } };
        
        var apiInstance = new TransactionalEmailsApi(config);

        var sender = new SendSmtpEmailSender("ExpenseTracker", providerSenderData.GetSection("FromMail").Get<string>()!);
        
        var to = new List<SendSmtpEmailTo> { new(toEmail, toName) };

        htmlContent += $"<br/><br/><a href=\"{configuration.GetSection("AppLink").Get<string>()}\">Expense Tracker</a>";
        
        var email = new SendSmtpEmail(
            sender: sender,
            to: to,
            subject: subject,
            htmlContent: htmlContent
        );

        await apiInstance.SendTransacEmailAsync(email);
    }
}