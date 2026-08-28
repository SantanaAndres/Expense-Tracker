using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class SmsComunicationService: IComunicationService
{
    public Task SendMessage() => throw new NotImplementedException();
}