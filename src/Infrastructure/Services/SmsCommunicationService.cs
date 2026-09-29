using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class SmsCommunicationService: ICommunicationService
{
    public string CommunicationServiceType => "Sms";

    public Task SendMessage() => throw new NotImplementedException();
}