using Application.Abstraction.Services;

namespace Infrastructure.Services;

public class EmailComunicationService: IComunicationService
{
    public Task SendMessage() => throw new NotImplementedException();
}