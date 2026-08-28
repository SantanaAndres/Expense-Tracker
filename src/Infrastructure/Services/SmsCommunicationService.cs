using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class SmsCommunicationService: ICommunicationService
{
    public Task SendMessage() => throw new NotImplementedException();
}