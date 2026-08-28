using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class EmailCommunicationService: ICommunicationService
{
    public Task SendMessage() => throw new NotImplementedException();
}