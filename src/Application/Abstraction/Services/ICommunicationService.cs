namespace Application.Abstraction.Services;

public interface ICommunicationService
{
    string CommunicationServiceType { get; }
    Task SendMessage();
}