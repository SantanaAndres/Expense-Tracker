using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class EmailCommunicationService: ICommunicationService
{
    public string CommunicationServiceType => "Email";
    public Task SendMessage() => throw new NotImplementedException();
}