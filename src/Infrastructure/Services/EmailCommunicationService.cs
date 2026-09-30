using Application.Abstraction.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Infrastructure.Services;

public class EmailCommunicationService(IConfiguration conf, ILogger<EmailCommunicationService> logger): ICommunicationService
{
    public string CommunicationServiceType => "Email";

    public async Task<bool> SendMessage(CommunicationServiceParameters parameters)
    {
        logger.LogInformation("Starting Email Communication Service");
        
        var brevoConfiguration = conf.GetSection("Brevo");
        
        var apiKey = brevoConfiguration.GetSection("ApiKey").Value;
        
        var senderMail = brevoConfiguration.GetSection("FromMail").Value;

        var appLink = conf.GetSection("AppLink").Value;

        if (apiKey is null)
            throw new NullReferenceException("Please config the ApiKey in the appsettings.json file");
        
        if(senderMail is null)
            throw new NullReferenceException("Please config the SenderMail in the appsettings.json file");
        
        if(appLink is null)
            throw new NullReferenceException("Please config the AppLink in the appsettings.json file");
        
        Configuration.Default.ApiKey["api-key"] = apiKey;

        var apiInstance = new TransactionalEmailsApi();

        string senderName = "Expense Tracker";
        
        SendSmtpEmailSender sender = new SendSmtpEmailSender(senderName, senderMail);

        string recipientEmail = parameters.To;
        SendSmtpEmailTo recipient = new SendSmtpEmailTo(recipientEmail, recipientEmail);
        List<SendSmtpEmailTo> toList = new List<SendSmtpEmailTo> { recipient };

        string subject = parameters.Subject;
        string htmlContent = $"<html><body><h1>Hello, Jane!</h1><p>Thanks for joining us {appLink}.</p></body></html>";
        string textContent = parameters.Message;

        var sendSmtpEmail = new SendSmtpEmail(
            sender: sender,
            to: toList,
            htmlContent: htmlContent,
            textContent: textContent,
            subject: subject
        );
        
        CreateSmtpEmail result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
        logger.LogInformation("Email sent successfully {Id}", result.MessageId);
        
        return true;
    }
}