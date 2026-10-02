using Application.Abstraction.Services;
using Application.Helper.Exceptions;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Infrastructure.Services;

public class SmsCommunicationService(IConfiguration conf): ICommunicationService
{
    public string CommunicationServiceType => "Sms";

    public async Task<bool> SendMessage(CommunicationServiceParameters parameters)
    {
        var brevoConf = conf.GetSection("Brevo");
        
        string apiKey = brevoConf.GetSection("ApiKey").Value;

        if (string.IsNullOrEmpty(apiKey))
            throw new NotFoundException("Please specify ApiKey");
        
        if (!parameters.To.All(char.IsDigit))
            throw new ArgumentException("This is not a valid phone number {phone}", parameters.To);
        
        Configuration.Default.ApiKey["api-key"] = apiKey;
        
        var apiInstance = new TransactionalSMSApi();

        var recipientPhoneNumber = string.Concat("+507", parameters.To);
        
        var sendSms = new SendTransacSms(
            sender: "ExpenseApp",             
            recipient: recipientPhoneNumber,
            content: parameters.Message,
            type: SendTransacSms.TypeEnum.Transactional
        );
        
        SendSms result = await apiInstance.SendTransacSmsAsync(sendSms);
        
        return true;
    }
}